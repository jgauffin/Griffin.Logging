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

using System.Diagnostics.Contracts;
using System.IO;
using System.Reflection;
using Griffin.Logging.Targets;
using Griffin.Logging.Targets.File;

namespace Griffin.Logging
{
	/// <summary>
	/// Contains configuration for all targets.
	/// </summary>
	/// <remarks>
	/// Create an extension method for all of your custom targets using this class as the instance object
	/// </remarks>
	public class FluentTargetConfigurationTypes
	{
		private readonly FluentTargetConfiguration _configuration;

		/// <summary>
		/// Initializes a new instance of the <see cref="FluentTargetConfigurationTypes"/> class.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		public FluentTargetConfigurationTypes(FluentTargetConfiguration configuration)
		{
			_configuration = configuration;
		}

		/// <summary>
		/// We are done with the tagret configurations.
		/// </summary>
		public FluentTargetConfiguration Done
		{
			get { return _configuration; }
		}

		/// <summary>
		/// Add a target
		/// </summary>
		/// <param name="target">Target to add</param>
		public void Add(ILogTarget target)
		{
			Contract.Requires(target != null);
			_configuration.AddInternal(target);
		}

		/// <summary>
		/// Add a console window logger
		/// </summary>
		/// <returns>Target configuration</returns>
		public FluentTargetConfiguration ConsoleLogger()
		{
			Add(new ConsoleTarget(new ConsoleConfiguration()));
			return _configuration;
		}

		/// <summary>
		/// Add a console window logger using custom settings
		/// </summary>
		/// <param name="config">custom configuration settings</param>
		/// <returns>Target configuration</returns>
		public FluentTargetConfiguration ConsoleLogger(ConsoleConfiguration config)
		{
			Add(new ConsoleTarget(config));

			return _configuration;
		}

		/// <summary>
		/// Add a file logger
		/// </summary>
		/// <param name="name">Target alias</param>
		/// <param name="config">Custom configuration</param>
		/// <returns>Target configuration</returns>
		public FluentTargetConfiguration FileLogger(string name, FileConfiguration config)
		{
			Add(new FileTarget(name, config));
			return _configuration;
		}

		/// <summary>
		/// Ass a file logger
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		public FluentTargetConfiguration FileLogger(string name)
		{
			var config = new FileConfiguration();
			config.Path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + "\\logs\\";
			config.DaysToKeep = 7;
			Add(new FileTarget(name, config));
			return _configuration;
		}

		/// <summary>
		/// Paddeds the file logger.
		/// </summary>
		/// <param name="name">File name without extension and path.</param>
		/// <param name="config">Custom configuration.</param>
		/// <returns>Target configuration</returns>
		public FluentTargetConfiguration PaddedFileLogger(string name, FileConfiguration config)
		{
			Add(new PaddedFileTarget(name, config));
			return _configuration;
		}

		/// <summary>
		/// File logger which uses columns for each property being logged.
		/// </summary>
		/// <param name="name">Filename without extension and path</param>
		/// <returns>Target configuration</returns>
		public FluentTargetConfiguration PaddedFileLogger(string name)
		{
			var config = new FileConfiguration
			             	{
			             		Path = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location) + "\\logs\\",
			             		DaysToKeep = 7
			             	};
			Add(new PaddedFileTarget(name, config));
			return _configuration;
		}
	}
}