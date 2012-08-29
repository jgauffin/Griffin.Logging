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

using System.Collections.Generic;

namespace Griffin.Logging
{
    /// <summary>
    /// Fluent configuration api for the logging library.
    /// </summary>
    /// <example>
    /// <code>
    /// new FluentConfiguration()
    ///    .LogNamespace("Griffin.Logging.Tests").AndSubNamespaces.ToTargetNamed("Console")
    ///    .AddTarget("Console").As.ConsoleLogger().Done
    ///    .Build();
    ///</code>
    /// </example>
    public class FluentConfiguration
    {
        private readonly List<FluentNamespaceLogging> _namespaces = new List<FluentNamespaceLogging>();
        private readonly List<FluentTargetConfiguration> _targets = new List<FluentTargetConfiguration>();


        /// <summary>
        /// Initializes a new instance of the <see cref="FluentConfiguration"/> class.
        /// </summary>
        public FluentConfiguration()
        {
        }

        /// <summary>
        /// Logg all namespaces
        /// </summary>
        public FluentNamespaceLogging LogEverything
        {
            get
            {
                var ns = new FluentNamespaceLogging(this, null);
                _namespaces.Add(ns);
                return ns;
            }
        }

        /// <summary>
        /// Configure a target that log entries can be written to
        /// </summary>
        /// <param name="name">Name of the target. Must be the same as used by <see cref="LogNamespace"/></param>
        /// <returns>Current configuration instance (to be able to configure fluently)</returns>
        public FluentTargetConfiguration AddTarget(string name)
        {
            var target = new FluentTargetConfiguration(this, name);
            _targets.Add(target);
            return target;
        }

        /// <summary>
        /// Log a specific name space to a named target (see <see cref="AddTarget"/> method)
        /// </summary>
        /// <param name="namespace">Namespace to log. No wildcards etc.</param>
        /// <returns>Current configuration instance (to be able to configure fluently)</returns>
        public FluentNamespaceLogging LogNamespace(string @namespace)
        {
            var ns = new FluentNamespaceLogging(this, @namespace);
            _namespaces.Add(ns);
            return ns;
        }

        /// <summary>
        /// Build the logging configuration and assign a log manager.
        /// </summary>
        /// <returns>Generated log manager</returns>
        /// <remarks>
        /// Call this method to generate the configuration. It will also assign the LogManager so you can start 
        /// logging after calling this method.
        /// </remarks>
        public FluentLogManager Build()
        {
            var logManager = new FluentLogManager();
            logManager.AddNamespaceFilters(_namespaces);
            logManager.AddTargets(_targets);
            LogManager.Assign(logManager);
            return logManager;
        }
    }
}