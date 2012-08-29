using System;

namespace Griffin.Logging.Filters
{
    /// <summary>
    /// Pre filters are used before a <see cref="LogEntry"/> class has been generated and thus before some heavy lifting has been made.
    /// </summary>
    /// <remarks>
    /// <para>
    /// There are two types of filters used in the logging framework.
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
    public interface IPreFilter
    {
        /// <summary>
        /// Determines if a log entry can be logged.
        /// </summary>
        /// <param name="loggedType">Type that is logging</param>
        /// <param name="logLevel">Log level</param>
        /// <returns><c>true</c> if the log entry can be logged; otherwise <c>false</c>.</returns>
        bool CanLog(Type loggedType, LogLevel logLevel);
    }
}