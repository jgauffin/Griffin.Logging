using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging.Filters
{
	[ContractClassFor(typeof (IPreFilter))]
	internal abstract class IPreFilterContract : IPreFilter
	{
		#region IPreFilter Members

		public bool CanLog(Type loggedType, LogLevel logLevel)
		{
			Contract.Requires(loggedType != null);
			return false;
		}

		#endregion
	}
}