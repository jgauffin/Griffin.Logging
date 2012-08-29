using System;

namespace Griffin.Logging.Targets
{
    /// <summary>
    /// Extensions for <see cref="LogEntry"/>
    /// </summary>
    public static class LogEntryExtensions
    {
        /// <summary>
        /// Build source using either stackframes or the logged type
        /// </summary>
        /// <param name="entry">Entry containing information</param>
        /// <returns>Formatted source</returns>
        public static string GetLoggerInfo(this LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");
            return string.Format("{0}.{1}()", entry.LoggedType.Name, (entry.MethodName ?? "[UnknownMethod]"));
        }
    }
}