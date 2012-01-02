/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Griffin.Logging.Filters;

namespace Griffin.Logging.Targets.File
{
	/// <summary>
	/// Target writing log entries to a file
	/// </summary>
	/// <remarks>
	/// This entry uses pipe symbol "|" to separate fields in each log entry. Each new line in a log
	/// entry is prepended with a tab character to let parsers to be able to detect when a log entry ends.
	/// </remarks>
	public class FileTarget : ILogTarget
	{
		private readonly IFileWriter _fileWriter;
		private readonly List<IPostFilter> _filters = new List<IPostFilter>();

		/// <summary>
		/// Initializes a new instance of the <see cref="FileTarget"/> class.
		/// </summary>
		/// <param name="name">File name without extension.</param>
		/// <param name="configuration">The configuration.</param>
		public FileTarget(string name, FileConfiguration configuration)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));
			Contract.Requires<ArgumentNullException>(configuration != null);

			_fileWriter = new FileWriter(name, configuration);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FileTarget"/> class.
		/// </summary>
		/// <param name="writer">Writer being used to to write log entries.</param>
		public FileTarget(IFileWriter writer)
		{
			Contract.Requires<ArgumentNullException>(writer != null);
			_fileWriter = writer;
		}

		/// <summary>
		/// File configuration
		/// </summary>
		protected FileConfiguration Configuration
		{
			get { return _fileWriter.Configuration; }
		}

		#region ILogTarget Members

		/// <summary>
		/// Gets a unique 
		/// </summary>
		public string Name
		{
			get { return _fileWriter.Name; }
		}

		/// <summary>
		/// Add a filter for this target.
		/// </summary>
		/// <param name="filter">Filters are used to validate if an entry can be written to a target or not.</param>
		public void AddFilter(IPostFilter filter)
		{
			_filters.Add(filter);
		}

		/// <summary>
		/// Enqueue to be written
		/// </summary>
		/// <param name="entry">Entry that should be written</param>
		public void Enqueue(LogEntry entry)
		{
			var entryString = FormatLogEntry(entry);
			_fileWriter.Write(entryString);
		}

		#endregion

		/// <summary>
		/// Format an exception
		/// </summary>
		/// <param name="exception">Exception (and it's nested innner exceptions) that will be formatted</param>
		/// <param name="intendation">Number of tabs to use for intendation</param>
		/// <returns>A string</returns>
		/// <remarks>
		/// Will call itself reursivly to be able it add all inner exceptions properly. Intendation will be
		/// increased for each found inner exception.
		/// </remarks>
		protected virtual string FormatException(Exception exception, int intendation)
		{
			Contract.Requires<ArgumentNullException>(exception != null);
			Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

			var text = "\r\n******* EXCEPTION ********\r\n"
			           + exception.ToString().Replace("\r\n", "\r\n".PadRight(intendation, '\t'));
			if (exception.InnerException != null)
				return text + FormatException(exception.InnerException, intendation + 1);
			return text;
		}

		/// <summary>
		/// Format a log entry as it should be written to the file
		/// </summary>
		/// <param name="entry">Entry to format</param>
		/// <returns>Formatted entry</returns>
		protected virtual string FormatLogEntry(LogEntry entry)
		{
			Contract.Requires<ArgumentNullException>(entry != null);
			Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

			if (entry.Exception != null)
			{
				return string.Format("{0}|{1}|{2}|{3}|{4}|{5}\r\n{6}\r\n",
				                     entry.CreatedAt.ToString(Configuration.DateTimeFormat),
				                     entry.LogLevel,
				                     entry.ThreadId,
				                     FormatUserName(entry.UserName, 40),
				                     FormatStackTrace(entry.StackFrames, 100),
				                     FormatMessage(entry.Message),
				                     FormatException(entry.Exception, 1)
					);
			}

			return string.Format("{0}|{1}|{2}|{3}|{4}|{5}\r\n",
			                     entry.CreatedAt.ToString(Configuration.DateTimeFormat),
			                     entry.LogLevel,
			                     entry.ThreadId,
			                     FormatUserName(entry.UserName, 40),
			                     FormatStackTrace(entry.StackFrames, 100),
			                     FormatMessage(entry.Message)
				);
		}

		/// <summary>
		/// Format the actual log message
		/// </summary>
		/// <param name="message">Message to format</param>
		/// <returns>New lines with be prefixed with a tab to be able to detect when an entry ends.</returns>
		protected virtual string FormatMessage(string message)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

			return message.Replace("\r\n", "\r\n\t").Replace('|', ';');
		}

		/// <summary>
		/// Format a stack trace (will only use the first frame)
		/// </summary>
		/// <param name="frames">Frames to format</param>
		/// <param name="maxSize">Max string length</param>
		/// <returns>Returns ClassName.MethodName if it's within the size limit, else the method name (might be truncated to fit size limit)</returns>
		protected virtual string FormatStackTrace(StackFrame[] frames, int maxSize)
		{
			var typeName = frames[0].GetMethod().ReflectedType.Name;
			var methodName = frames[0].GetMethod().Name;
			var result = string.Format("{0}.{1}", typeName, methodName);
			if (result.Length < maxSize)
				return result;

			return MaxSize(methodName, maxSize);
		}

		/// <summary>
		/// Format user name
		/// </summary>
		/// <param name="userName">Username (can include domain using format DOMAIN\\UserName)</param>
		/// <param name="maxSize">Maximum number of characters than can be used</param>
		/// <returns>Formatted user name</returns>
		/// <remarks>
		/// Will shrink user names if they are too large. First attemp is to remove domain and the second
		/// attempt simply cuts the username.
		/// </remarks>
		protected virtual string FormatUserName(string userName, int maxSize)
		{
			if (userName.Length > 0)
			{
				var pos = userName.IndexOf('\\'); //domain name
				if (pos != -1)
					userName = userName.Substring(pos + 1);
			}

			return MaxSize(userName, maxSize);
		}

		/// <summary>
		/// Check if a string is withing the specified size
		/// </summary>
		/// <param name="value">String to check</param>
		/// <param name="size">Max size (inclusive)</param>
		/// <returns>Original string if its within the limit, else a truncated string with "." as suffix to indicate truncation.</returns>
		protected string MaxSize(string value, int size)
		{
			if (value.Length > size)
				return value.Substring(0, size - 1) + ".";
			return value;
		}
	}
}