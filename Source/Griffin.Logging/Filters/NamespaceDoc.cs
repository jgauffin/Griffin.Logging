namespace Griffin.Logging.Filters
{
	/// <summary>
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
	/// </summary>
	internal class NamespaceDoc
	{
	}
}