namespace Griffin.Logging.Net
{
    /// <summary>
    /// Implement this interface to receive all log entries from the queue
    /// </summary>
    /// <remarks>Typically done server side ;)</remarks>
    public interface ILogReceiver
    {
        /// <summary>
        /// Received a new log entry.
        /// </summary>
        /// <param name="dto">Log entry</param>
        void ReceivedLogEntry(LogEntryDTO dto);
    }
}