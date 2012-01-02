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
using System.Globalization;
using System.IO;
using System.Threading;

namespace Griffin.Logging.Targets.File
{
	/// <summary>
	/// Used to write entries to files-
	/// </summary>
	/// <remarks>
	/// Will not keep the file open but open it using <c>File.AppendAllText</c> each time a new entry should be written.
	/// </remarks>
	public class FileWriter : IFileWriter
	{
		private readonly FileConfiguration _configuration;
		private readonly string _name;
		private string _fileName;
		private DateTime _logDate;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileWriter"/> class.
		/// </summary>
		/// <param name="name">Fle name.</param>
		/// <param name="configuration">The configuration.</param>
		public FileWriter(string name, FileConfiguration configuration)
		{
			_name = name;
			_configuration = configuration;
			_logDate = DateTime.Today;
		}

		#region IFileWriter Members

		/// <summary>
		/// Gets the configuration.
		/// </summary>
		public FileConfiguration Configuration
		{
			get { return _configuration; }
		}


		/// <summary>
		/// Gets the name.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Writes the specified log entry.
		/// </summary>
		/// <param name="logEntry">The log entry.</param>
		public void Write(string logEntry)
		{
			var attempts = 0;
			try
			{
				while (true)
				{
					try
					{
						System.IO.File.AppendAllText(BuildFileName(), logEntry);
						break;
					}
					catch (IOException err)
					{
						Thread.Sleep(10);
						++attempts;
						if (attempts >= 5)
						{
							Console.WriteLine("Failed to write to log {0}: " + err, BuildFileName());
							//if (ExceptionPolicyHandler.GotUnhandled(this, err) == ExceptionPolicy.Throw)
							//    throw;
							break;
						}
					}
				}

				CheckOldFolder();
			}
			catch (Exception err)
			{
				Console.WriteLine("Failed to write to log {0}: " + err, BuildFileName());
				//if (ExceptionPolicyHandler.GotUnhandled(this, err) == ExceptionPolicy.Throw)
				//    throw;
			}
		}

		#endregion

		private string BuildFileName()
		{
			if (_logDate.Date == DateTime.Today && _fileName != null)
				return _fileName;

			var fullPath = Configuration.Path;
			if (!fullPath.EndsWith("\\"))
				fullPath += "\\";
			if (!Directory.Exists(fullPath))
				Directory.CreateDirectory(fullPath);

			if (Configuration.CreateDateFolder)
			{
				fullPath += DateTime.Today.ToString(Configuration.DateFormat).Replace('/', '-') + "\\";
				if (!Directory.Exists(fullPath))
					Directory.CreateDirectory(fullPath);
			}

			fullPath += _name + ".log";
			_fileName = fullPath;
			return fullPath;
		}

		/// <summary>
		/// Is NOT thread safe.
		/// </summary>
		private void CheckOldFolder()
		{
			if (_logDate == DateTime.Today)
				return;

			_logDate = DateTime.Today;
			RemoveOldDirectories();
		}

		private void RemoveOldDirectories()
		{
			if (_configuration.DaysToKeep == 0)
				return;

			var minDate = DateTime.Now.AddDays(0 - _configuration.DaysToKeep);
			var directories = Directory.GetDirectories(_configuration.Path);
			foreach (var directory in directories)
			{
				var pos = directory.LastIndexOf(Path.DirectorySeparatorChar);
				var name = directory.Substring(pos + 1);

				DateTime folderDate;
				if (
					!DateTime.TryParseExact(name, _configuration.DateFormat, CultureInfo.InvariantCulture,
					                        DateTimeStyles.None, out folderDate))
					continue;

				if (folderDate < minDate)
				{
					Directory.Delete(directory, true);
				}
			}
		}
	}
}