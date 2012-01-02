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
using System.Diagnostics.Contracts;
using System.Linq;
using Griffin.Logging.Filters;

namespace Griffin.Logging.Targets
{
	/// <summary>
	/// Container for two or more log targets
	/// </summary>
	public class CompositeTarget : ILogTarget
	{
		private readonly List<IPostFilter> _filters = new List<IPostFilter>();
		private readonly string _name;
		private readonly List<ILogTarget> _targets = new List<ILogTarget>();

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeTarget"/> class.
		/// </summary>
		/// <param name="name">Name of this target.</param>
		public CompositeTarget(string name)
		{
			Contract.Requires(!String.IsNullOrEmpty(name));

			_name = name;
		}

		/// <summary>
		/// Gets the targets used to write to the log files
		/// </summary>
		public List<ILogTarget> Targets
		{
			get { return _targets; }
		}

		#region ILogTarget Members

		/// <summary>
		/// Gets name of this target
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Add a filter for this target.
		/// </summary>
		/// <param name="filter">Filters are used to validate if an entry can be written to a target or not.</param>
		public void AddFilter(IPostFilter filter)
		{
			_filters.Add(filter);
		}


		/// <summary>
		/// Enqueue to be written
		/// </summary>
		/// <param name="entry"></param>
		/// <remarks>
		/// <para>
		/// Will enqueue the entry in each target that was added to this container. The entry will be added
		/// using the same order as when all targets were added.
		/// </para>
		/// <para>
		/// The entry might be written directly in the same thread or enqueued to be written
		/// later. It's up to each implementation to decide. Keep in mind that a logger should not
		/// introduce delays in the thread execution. If it's possible that it will delay the thread,
		/// enqueue entries instead and write them in a seperate thread.
		/// </para>
		/// </remarks>
		public void Enqueue(LogEntry entry)
		{
			if (_filters.Any(f => !f.CanLog(entry)))
				return;

			foreach (var target in _targets)
			{
				target.Enqueue(entry);
			}
		}

		#endregion
	}
}