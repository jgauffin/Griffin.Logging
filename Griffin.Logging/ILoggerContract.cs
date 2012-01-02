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
using System.Diagnostics.Contracts;

namespace Griffin.Logging
{
// ReSharper disable InconsistentNaming
	[ContractClassFor(typeof (ILogger))]
	internal abstract class ILoggerContract : ILogger
// ReSharper restore InconsistentNaming
	{
		#region ILogger Members

		public void Debug(string message)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Debug(string message, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Debug(string message, Exception exception)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		public void Debug(string message, Exception exception, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		public void Error(string message)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Error(string message, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Error(string message, Exception exception)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		public void Error(string message, Exception exception, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		public void Info(string message)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Info(string message, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Info(string message, Exception exception)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		public void Info(string message, Exception exception, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		public void Warning(string message)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Warning(string message, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
		}

		public void Warning(string message, Exception exception)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		public void Warning(string message, Exception exception, params object[] formatters)
		{
			Contract.Requires<ArgumentNullException>(message != null);
			Contract.Requires<ArgumentNullException>(exception != null);
		}

		#endregion
	}
}