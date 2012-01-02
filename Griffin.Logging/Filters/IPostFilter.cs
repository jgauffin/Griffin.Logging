using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Griffin.Logging.Filters
{
	/// <summary>
	/// Post filters are used after a <see cref="LogEntry"/> class has been generated including its <see cref="StackTrace"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// There are two types of filters used. 
	/// </para>
	/// <para>
	/// One which can filter on the type being logged and the specified log level. These
	/// filters are called PreFilters since they are being run before a <see cref="LogEntry"/> has been generated (and thus
	/// saving a bit of CPU which will be wasted when a StackTrace is being generated.
	/// </para>
	/// <para>
	/// The other type is called PostFilters since they are being invoked after all information have been collected, but before
	/// anything has been written to the log targets.
	/// </para>
	/// </remarks>
	[ContractClass(typeof (IPostFilterContract))]
	public interface IPostFilter
	{
		/// <summary>
		/// Check a log entry
		/// </summary>
		/// <param name="entry">Entry to validate</param>
		/// <returns>true if entry can be written to a log; otherwise false.</returns>
		bool CanLog(LogEntry entry);
	}
}