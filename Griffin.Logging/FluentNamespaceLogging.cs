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

namespace Griffin.Logging
{
	/// <summary>
	/// Used to configure wich target(s) each namespace should usee.
	/// </summary>
	/// <example>
	/// <code>
	/// Configure.Griffin.Logging()
	///     .LogNamespace("Griffin").AndChildNamespaces.ToTargetName("Console").Done
	///     .AddTarget("Console").As.ConsoleLogger
	///     .Build();
	/// </code>
	/// 
	/// </example>
	public class FluentNamespaceLogging
	{
		private readonly FluentConfiguration _configuration;
		private readonly List<string> _targets = new List<string>();

		/// <summary>
		/// Initializes a new instance of the <see cref="FluentNamespaceLogging"/> class.
		/// </summary>
		/// <param name="configuration">Base logging configuration.</param>
		/// <param name="name">Namespace name.</param>
		public FluentNamespaceLogging(FluentConfiguration configuration, string name)
		{
			Name = name;
			_configuration = configuration;
		}

		/// <summary>
		/// Gets if chld namespaces should be included.
		/// </summary>
		public FluentNamespaceLogging AndChildNamespaces
		{
			get
			{
				LogChildNamespaces = true;
				return this;
			}
		}

		/// <summary>
		/// Name space configuration is completed
		/// </summary>
		public FluentConfiguration Done
		{
			get { return _configuration; }
		}

		/// <summary>
		/// Gets name of the namespace.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Log child namespaces.
		/// </summary>
		internal bool LogChildNamespaces { get; private set; }

		/// <summary>
		/// Gets all targets
		/// </summary>
		internal IEnumerable<string> Targets
		{
			get { return _targets; }
		}

		/// <summary>
		/// Log to a specific target
		/// </summary>
		/// <param name="name">Target alias</param>
		/// <returns>Base logging configuration</returns>
		/// <seealso cref="FluentTargetConfiguration"/>.
		public FluentConfiguration ToTargetNamed(string name)
		{
			_targets.Add(name);
			return _configuration;
		}

		/// <summary>
		/// Log this namespace to several targets
		/// </summary>
		/// <param name="names">Target aliases.</param>
		/// <returns>Base logging configuration</returns>
		/// <seealso cref="FluentTargetConfiguration"/>.
		public FluentConfiguration ToTargetsNamed(params string[] names)
		{
			_targets.AddRange(names);
			return _configuration;
		}

		/// <summary>
		/// Check if this namespace configuration is valid for the specified type.
		/// </summary>
		/// <param name="type">Type to validate</param>
		/// <returns><c>true</c> if this namespace can log the specified type; otherwise <c>false</c>.</returns>
		public bool IsForType(Type type)
		{
			return LogChildNamespaces ? Name.StartsWith(type.Namespace) : Name == type.Namespace;
		}
	}
}