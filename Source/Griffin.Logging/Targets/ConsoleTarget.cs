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
using System.Diagnostics.Contracts;
using Griffin.Logging.Filters;

namespace Griffin.Logging.Targets
{
	/// <summary>
	/// Write entry to the console.
	/// </summary>
	public class ConsoleTarget : ILogTarget
	{
		private readonly ConsoleConfiguration _configuration;
		private readonly List<IPostFilter> _filters = new List<IPostFilter>();

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleTarget"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public ConsoleTarget(ConsoleConfiguration configuration)
		{
			Contract.Requires<ArgumentNullException>(configuration != null);

			_configuration = configuration;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleTarget"/> class.
		/// </summary>
		public ConsoleTarget()
		{
			_configuration = new ConsoleConfiguration();
		}

		#region ILogTarget Members

		/// <summary>
		/// Gets name of target.
		/// </summary>
		public string Name
		{
			get { return "Console"; }
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
		/// <param name="entry"></param>
		/// <remarks>
		/// The entry might be written directly in the same thread or enqueued to be written
		/// later. It's up to each implementation to decide. Keep in mind that a logger should not
		/// introduce delays in the thread execution. If it's possible that it will delay the thread,
		/// enqueue entries instead and write them in a seperate thread.
		/// </remarks>
		public void Enqueue(LogEntry entry)
		{
			var method = entry.StackFrameOrType();

			var tmp = String.Format("{0} {1} {2} {3} {4}",
			                        entry.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff"),
			                        entry.ThreadId.ToString().PadLeft(3),
			                        entry.UserName.PadRight(25),
			                        method.PadRight(40),
			                        entry.Message.Replace("\r\n", "\r\n\t"));

			Console.ForegroundColor = GetColor(entry);
			Console.WriteLine(tmp);
			if (entry.Exception != null)
			{
				var exception = entry.Exception;
				var tabs = "\t";
				while (exception != null)
				{
					Console.WriteLine(tabs + entry.Exception.ToString().Replace("\r\n", "\r\n" + tabs));

					exception = exception.InnerException;
					tabs += "\t";
				}
			}
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		#endregion

		/// <summary>
		/// Get color that should be used for a log entry.
		/// </summary>
		/// <param name="entry">Entry to be colored</param>
		/// <returns>Console color</returns>
		protected virtual ConsoleColor GetColor(LogEntry entry)
		{
			Contract.Requires<ArgumentNullException>(entry != null);

			switch (entry.LogLevel)
			{
				case LogLevel.Debug:
					return _configuration.DebugColor;
				case LogLevel.Info:
					return _configuration.InfoColor;
				case LogLevel.Warning:
					return _configuration.WarningColor;
				case LogLevel.Error:
					return _configuration.ErrorColor;
				default:
					return ConsoleColor.Gray;
			}
		}
	}
}