using System.Diagnostics.Contracts;

namespace Griffin.Logging.Filters
{
	[ContractClassFor(typeof (IPostFilter))]
// ReSharper disable InconsistentNaming
	internal abstract class IPostFilterContract : IPostFilter
// ReSharper restore InconsistentNaming
	{
		#region IPostFilter Members

		public bool CanLog(LogEntry entry)
		{
			Contract.Requires(entry != null);
			return false;
		}

		#endregion
	}
}