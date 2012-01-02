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
using System.Diagnostics;

namespace Griffin.Logging
{
	/// <summary>
	/// entry that should be written to a file
	/// </summary>
	public class LogEntry
	{
		/// <summary>
		/// Gets or sets name of the current identity.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets when entry was created
		/// </summary>
		public DateTime CreatedAt { get; set; }

		/// <summary>
		/// Gets or sets type that has the logger as a member
		/// </summary>
		public Type LoggedType { get; set; }

		/// <summary>
		/// Gets or sets stack frames
		/// </summary>
		public StackFrame[] StackFrames { get; set; }

		/// <summary>
		/// Gets or sets log message.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets exception (optional)
		/// </summary>
		public Exception Exception { get; set; }

		/// <summary>
		/// Gets or sets id of current thread.
		/// </summary>
		public int ThreadId { get; set; }

		/// <summary>
		/// Gets or sets how important the log entry is
		/// </summary>
		/// <remarks>
		/// Here is our recommendation to how you should use each log level.
		/// <list type="table">
		/// <item>
		/// <term>Debug</term>
		/// <description>Debug entries are usually used only when debugging. They can be used to track
		/// variables or method contracts. There might be several debug entries per method.</description>
		/// </item>
		/// <item>
		/// <term>Info</term>
		/// <description>Informational messages are used to track state changes such as login, logout, record updates etc. 
		/// There are at most one entry per method.</description>
		/// </item>
		/// <item>
		/// <term>Warning</term>
		/// <description>
		/// Warnings are used when something unexpected happend but the application can handle it and continue as expected.
		/// </description>
		/// </item>
		/// <item>
		/// <term>Error</term>
		/// <description>
		/// Errors are when something unexpected happens and the application cannot deliver result as expected. It might or might not
		/// mean that the application has to be restarted.
		/// </description>
		/// </item>
		/// </list>
		/// </remarks>
		public LogLevel LogLevel { get; set; }
	}
}