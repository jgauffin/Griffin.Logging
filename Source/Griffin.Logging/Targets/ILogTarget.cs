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

using System.Diagnostics.Contracts;
using Griffin.Logging.Filters;

namespace Griffin.Logging.Targets
{
	/// <summary>
	/// Log target
	/// </summary>
	/// <remarks>
	/// A destination that log entries will be written to.
	/// </remarks>
	[ContractClass(typeof (ILogTargetContract))]
	public interface ILogTarget
	{
		/// <summary>
		/// Gets name of target. 
		/// </summary>
		/// <remarks>
		/// It must be unique for each target. The filename works for file targets etc.
		/// </remarks>
		string Name { get; }

		/// <summary>
		/// Add a filter for this target.
		/// </summary>
		/// <param name="filter">Filters are used to validate if an entry can be written to a target or not.</param>
		void AddFilter(IPostFilter filter);

		/// <summary>
		/// Enqueue to be written
		/// </summary>
		/// <param name="entry"></param>
		/// <remarks>
		/// The entry might be written directly in the same thread or enqueued to be written
		/// later. It's up to each implementation to decide. Keep in mind that a logger should not
		/// introduce delays in the thread execution. If it's possible that it will delay the thread,
		/// enqueue entries instead and write them in a seperate thread.
		/// </remarks>
		void Enqueue(LogEntry entry);
	}
}