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
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Griffin.Logging.Targets;

namespace Griffin.Logging
{
	/// <summary>
	/// Log manager which is configured using the fluent interface.
	/// </summary>
	internal class FluentLogManager : ILogManager
	{
		private List<FluentNamespaceLogging> _namespaces;
		private List<FluentTargetConfiguration> _targets;

		/// <summary>
		/// Initializes a new instance of the <see cref="FluentLogManager"/> class.
		/// </summary>
		/// <remarks>
		/// Will assign itself to the <see cref="LogManager"/> class.
		/// </remarks>
		public FluentLogManager()
		{
			LogManager.Assign(this);
		}

		#region ILogManager Members

		/// <summary>
		/// Get a logger for the specified type
		/// </summary>
		/// <param name="type">Type that requests a logger</param>
		/// <returns>
		/// A logger (always)
		/// </returns>
		public ILogger GetLogger(Type type)
		{
			var targets = new List<ILogTarget>();

			foreach (var ns in _namespaces)
			{
				if (!ns.IsForType(type))
					continue;

				foreach (var targetName in ns.Targets)
				{
					var name = targetName;
					var targetConfig = _targets.FirstOrDefault(tg => tg.Name == name);
					if (targetConfig == null)
						throw new ConfigurationErrorsException("Failed to find target named '" + name +
						                                       "', you must configure targets first.");

					foreach (var target in targetConfig.Targets)
					{
						var target1 = target;
						if (!targets.Any(t => t.Name == target1.Name))
							targets.Add(target);
					}
				}
			}

			return new Logger(type, targets);
		}

		#endregion

		private IEnumerable<ILogTarget> GetTargets(IEnumerable<string> names)
		{
			var targets = new List<ILogTarget>();
			foreach (var targetName in names)
			{
				foreach (var target in _targets)
				{
					if (target.Name != targetName)
						continue;

					foreach (var t in target.Targets)
						if (!targets.Contains(t))
							targets.Add(t);
					break;
				}
			}
			return targets;
		}

		internal void AddNamespaceFilters(List<FluentNamespaceLogging> namespaces)
		{
			_namespaces = namespaces;
		}

		internal void AddTargets(List<FluentTargetConfiguration> targets)
		{
			_targets = targets;
		}
	}
}