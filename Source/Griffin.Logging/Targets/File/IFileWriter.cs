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

namespace Griffin.Logging.Targets.File
{
	/// <summary>
	/// Used to write a log file to a file
	/// </summary>
	public interface IFileWriter
	{
		/// <summary>
		/// Gets configuration that is used.
		/// </summary>
		FileConfiguration Configuration { get; }

		/// <summary>
		/// Gets file name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Write an entry to the file
		/// </summary>
		/// <param name="logEntry">Entry to write.</param>
		void Write(string logEntry);
	}
}