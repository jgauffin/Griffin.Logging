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
using System.IO;
using Griffin.Logging.Filters;
using Griffin.Logging.Targets;
using Griffin.Logging.Targets.File;

namespace Griffin.Logging
{
	/// <summary>
	/// Log manager with built in configuration
	/// </summary>
	/// <remarks>
	/// Log manager that will return the same logger for all classes that requests one. You can however use
	/// <see cref="IPostFilter"/>s to determine which classes may log or not.
	/// </remarks>
	public class SimpleLogManager : ILogManager
	{
		private static SimpleLogManager _instance;
		private static readonly List<IPreFilter> Filters = new List<IPreFilter>();
		private static readonly List<ILogTarget> Targets = new List<ILogTarget>();

		/// <summary>
		/// Prevents a default instance of the <see cref="SimpleLogManager"/> class from being created.
		/// </summary>
		private SimpleLogManager()
		{
		}

		#region ILogManager Members

		/// <summary>
		/// Get a logger for the specified type
		/// </summary>
		/// <param name="type">Type that requests a logger</param>
		/// <returns>
		/// A logger (always)
		/// </returns>
		public ILogger GetLogger(Type type)
		{
			return new Logger(type, Filters, Targets);
		}

		#endregion

		/// <summary>
		/// Add a new log file
		/// </summary>
		/// <param name="fileName">Target file without path (Path is configured in <paramref name="configuration"/>).</param>
		/// <param name="configuration">Configuration used to control how entries should be written to the file</param>
		public static void AddFile(string fileName, FileConfiguration configuration)
		{
			CreateIfNeeded();
			Targets.Add(new PaddedFileTarget(fileName, configuration));
		}

		/// <summary>
		/// Create a singleton instance if it hasn't been created.
		/// </summary>
		private static void CreateIfNeeded()
		{
			if (_instance == null)
			{
				_instance = new SimpleLogManager();
				LogManager.Assign(_instance);
			}
		}

		/// <summary>
		/// Adds the file.
		/// </summary>
		/// <param name="fileName">Absolute or application relative path to the log file</param>
		/// <remarks>
		/// Will used the default settings for <see cref="FileConfiguration"/>.
		/// </remarks>
		public static void AddFile(string fileName)
		{
			var configuration = new FileConfiguration
			                    	{
			                    		Path = Path.IsPathRooted(fileName)
			                    		       	? Path.GetDirectoryName(fileName)
			                    		       	: AppDomain.CurrentDomain.BaseDirectory
			                    	};
			AddFile(Path.GetFileNameWithoutExtension(fileName), configuration);
		}

		/// <summary>
		/// Add a console window.
		/// </summary>
		/// <param name="createConsole">Create a console window if one hasn't been allocated (useful for Windows forms)</param>
		public static void AddConsole(bool createConsole)
		{
			CreateIfNeeded();
			Targets.Add(new ConsoleTarget());
			if (!ConsoleHelper.HasConsole && createConsole)
				ConsoleHelper.CreateConsole();
		}

		/// <summary>
		/// Adds a filter to a log target
		/// </summary>
		/// <param name="targetName">Name of the target (filename without path and extension, or "console" for the console).</param>
		/// <param name="logFilter">The log filter.</param>
		public static void AddFilter(string targetName, IPreFilter logFilter)
		{
			//Targets.First(t => Path.GetFileNameWithoutExtension(t.Name) == targetName).AddFilter(logFilter);
		}
	}
}