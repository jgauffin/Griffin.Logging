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

namespace Griffin.Logging
{
	/// <summary>
	/// Log levels
	/// </summary>
	public enum LogLevel
	{
		/// <summary>
		/// Debug entries are usually used only when debugging. They can be used to track
		/// variables or method contracts. There might be several debug entries per method
		/// </summary>
		Debug,

		/// <summary>
		/// Informational messages are used to track state changes such as login, logout, record updates etc. 
		/// There are at most one entry per method
		/// </summary>
		Info,

		/// <summary>
		/// Warnings are used when something unexpected happend but the application can handle it and continue as expected.
		/// </summary>
		Warning,

		/// <summary>
		/// Errors are when something unexpected happens and the application cannot deliver result as expected. It might or might not
		/// mean that the application has to be restarted.
		/// </summary>
		Error
	}
}