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

using System.Configuration;
using Griffin.Logging.Targets;

namespace Griffin.Logging
{
    /// <summary>
    /// Extension used to configure the database
    /// </summary>
    public static class TargetAdoNetExtension
    {
        /// <summary>
        /// Adds a database logger.
        /// </summary>
        /// <param name="instance">fluent configuration instance.</param>
        /// <param name="connectionStringName">Name of the connection string in app/webb.config.</param>
        /// <returns>Fluent configuration instance</returns>
        /// <remarks>
        /// Use the standard .NET way to define the connection string. This code will use <see cref="ConfigurationManager"/> to find the
        /// connection string in your config file.
        /// </remarks>
        public static FluentTargetConfiguration DatabaseLogger(this FluentTargetConfigurationTypes instance,
                                                               string connectionStringName)
        {
            instance.Add(new AdoNetTarget(connectionStringName));
            return instance.Done;
        }
    }
}