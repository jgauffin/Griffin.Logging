using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging
{
// ReSharper disable InconsistentNaming
	[ContractClassFor(typeof (ILogManager))]
	internal abstract class ILogManagerContract : ILogManager
// ReSharper restore InconsistentNaming
	{
		#region ILogManager Members

		public ILogger GetLogger(Type type)
		{
			Contract.Requires<ArgumentNullException>(type != null);
			Contract.Ensures(Contract.Result<ILogger>() != null);
			return null;
		}

		#endregion
	}
}