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
using Griffin.Logging.Targets;

namespace Griffin.Logging
{
	/// <summary>
	/// Define a target
	/// </summary>
	/// <remarks>
	/// FluentTargetConfiguration is used to group one or more <see cref="ILogTarget"/>(s) together.
	/// </remarks>
	public class FluentTargetConfiguration
	{
		private readonly string _name;
		private readonly Dictionary<string, ILogTarget> _targets = new Dictionary<string, ILogTarget>();
		private ILogTarget _currentTarget;

		/// <summary>
		/// Initializes a new instance of the <see cref="FluentTargetConfiguration"/> class.
		/// </summary>
		/// <param name="configuration">Main configuration class.</param>
		/// <param name="name">Alias for this target</param>
		public FluentTargetConfiguration(FluentConfiguration configuration, string name)
		{
			As = new FluentTargetConfigurationTypes(this);
			Done = configuration;
			_name = name;
		}

		/// <summary>
		/// Gets alias name (as used by namespace definitions)
		/// </summary>
		internal string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Gets all targets that where created for this logger.
		/// </summary>
		public IEnumerable<ILogTarget> Targets
		{
			get { return _targets.Values; }
		}


		/// <summary>
		/// Define which destinations this target should have
		/// </summary>
		public FluentTargetConfigurationTypes As { get; private set; }

		/// <summary>
		/// Target configuration is done
		/// </summary>
		public FluentConfiguration Done { get; private set; }

		/// <summary>
		/// Add filters to this target
		/// </summary>
		public FluentFilterConfiguration Filter
		{
			get { return new FluentFilterConfiguration(this, _currentTarget); }
		}

		/// <summary>
		/// Called by <see cref="FluentTargetConfigurationTypes"/> for each defined target
		/// </summary>
		/// <param name="target">Target to add</param>
		internal void AddInternal(ILogTarget target)
		{
			_currentTarget = target;
			if (_targets.ContainsKey(target.Name))
				return;

			_targets.Add(target.Name, target);
		}
	}
}