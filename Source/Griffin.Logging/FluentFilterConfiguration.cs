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

using Griffin.Logging.Filters;
using Griffin.Logging.Targets;

namespace Griffin.Logging
{
    /// <summary>
    /// Used to configure filters for a specific target.
    /// </summary>
    public class FluentFilterConfiguration
    {
        private readonly FluentTargetConfiguration _configuration;
        private readonly ILogTarget _logTarget;

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentFilterConfiguration"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logTarget">The log target.</param>
        public FluentFilterConfiguration(FluentTargetConfiguration configuration, ILogTarget logTarget)
        {
            _configuration = configuration;
            _logTarget = logTarget;
        }


        /// <summary>
        /// Filer on a level interval
        /// </summary>
        /// <param name="minimum">Minimum level, inclusive</param>
        /// <param name="maximum">Maximum level, inclusive</param>
        /// <returns>Target configuration</returns>
        public FluentTargetConfiguration OnLogLevelBetween(LogLevel minimum, LogLevel maximum)
        {
            _logTarget.AddFilter(new LevelFilter(minimum, maximum));
            return _configuration;
        }

        /// <summary>
        /// Filter on a specific level
        /// </summary>
        /// <param name="level">Level tat should be written to the target</param>
        /// <returns>Target configuration</returns>
        public FluentTargetConfiguration OnLogLevel(LogLevel level)
        {
            _logTarget.AddFilter(new LevelFilter(level, level));
            return _configuration;
        }
    }
}