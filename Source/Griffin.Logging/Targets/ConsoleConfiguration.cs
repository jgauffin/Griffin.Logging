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

namespace Griffin.Logging.Targets
{
    /// <summary>
    /// Settings used to control the log output to the console
    /// </summary>
    public class ConsoleConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleConfiguration"/> class.
        /// </summary>
        public ConsoleConfiguration()
        {
            DebugColor = ConsoleColor.Gray;
            ErrorColor = ConsoleColor.Red;
            WarningColor = ConsoleColor.Magenta;
            InfoColor = ConsoleColor.White;
        }

        /// <summary>
        /// Gets or sets color of debug messages
        /// </summary>
        /// <value>
        /// Default is Gray
        /// </value>
        public ConsoleColor DebugColor { get; set; }

        /// <summary>
        /// Gets or sets color of error messages
        /// </summary>
        /// <value>
        /// Default is Red
        /// </value>
        public ConsoleColor ErrorColor { get; set; }

        /// <summary>
        /// Gets or sets color of information messages
        /// </summary>
        /// <value>
        /// Default is White
        /// </value>
        public ConsoleColor InfoColor { get; set; }

        /// <summary>
        /// Gets or sets color of warning messages
        /// </summary>
        /// <value>
        /// Default is Magenta.
        /// </value>
        public ConsoleColor WarningColor { get; set; }
    }
}