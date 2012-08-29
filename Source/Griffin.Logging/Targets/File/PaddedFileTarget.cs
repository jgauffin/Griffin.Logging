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
    /// File logger using padding for each column.
    /// </summary>
    /// <remarks>
    /// Write a column based output to a file. Each column value is automatically truncated to fit
    /// in a column
    /// </remarks>
    public class PaddedFileTarget : FileTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaddedFileTarget"/> class.
        /// </summary>
        /// <param name="name">File name without extension</param>
        /// <param name="configuration">The configuration.</param>
        public PaddedFileTarget(string name, FileConfiguration configuration) : base(name, configuration)
        {
        }

        /// <summary>
        /// Format a log entry as it should be written to the file
        /// </summary>
        /// <param name="entry">Entry to format</param>
        /// <returns>
        /// Formatted entry
        /// </returns>
        protected override string FormatLogEntry(LogEntry entry)
        {
            if (entry.Exception != null)
            {
                return string.Format("{0} {1} {2} {3} {4} {5}\r\n{6}\r\n",
                                     entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                                     entry.LogLevel.ToString().PadRight(8, ' '),
                                     entry.ThreadId.ToString("000"),
                                     FormatUserName(entry.UserName, 16).PadRight(16),
                                     FormatCallingMethod(entry.LoggedType, entry.MethodName, 40).PadRight(40),
                                     FormatMessage(entry.Message),
                                     FormatException(entry.Exception, 1)
                    );
            }

            return string.Format("{0} {1} {2} {3} {4} {5}\r\n",
                                 entry.CreatedAt.ToString(Configuration.DateTimeFormat),
                                 entry.LogLevel.ToString().PadRight(8, ' '),
                                 entry.ThreadId.ToString("000"),
                                 FormatUserName(entry.UserName, 16).PadRight(16),
                                 FormatCallingMethod(entry.LoggedType, entry.MethodName, 40).PadRight(40),
                                 FormatMessage(entry.Message)
                );
        }
    }
}