namespace Griffin.Logging.Filters
{
	/// <summary>
	/// Log entries must be between (or equal to) the specified limits.
	/// </summary>
	public class LevelFilter : IPostFilter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LevelFilter"/> class.
		/// </summary>
		/// <param name="minLevel">Minimum (inclusive) level that can be logged.</param>
		/// <param name="maxLevel">Maximum (inclusive) level that can be logged.</param>
		public LevelFilter(LogLevel minLevel, LogLevel maxLevel)
		{
			MinLevel = minLevel;
			MaxLevel = maxLevel;
		}

		/// <summary>
		/// Gets or sets minimum (inclusive) log level of log entries that should be logged.
		/// </summary>
		/// <remarks>
		/// Check the actual enum to see which one is the lowest and largest values.
		/// </remarks>
		public LogLevel MinLevel { get; set; }

		/// <summary>
		/// Gets or sets maximum (inclusive) log level of log entries that should be logged.
		/// </summary>
		/// <remarks>
		/// Check the actual enum to see which one is the lowest and largest values.
		/// </remarks>
		public LogLevel MaxLevel { get; set; }

		#region IPostFilter Members

		/// <summary>
		/// Determines if the specified entry got an allowed log level
		/// </summary>
		/// <param name="entry">Entry to validate against the configured log levels</param>
		/// <returns>true if the entry can be logged; otherwise false.</returns>
		public bool CanLog(LogEntry entry)
		{
			return entry.LogLevel >= MinLevel && entry.LogLevel <= MaxLevel;
		}

		#endregion
	}
}