using System.Collections.Generic;
using System.Threading;
using Griffin.Logging.Net;

namespace Griffin.Logging.MQ.Tests
{
    public class Receiver : ILogReceiver
    {
        public List<LogEntryDTO> Entries = new List<LogEntryDTO>();
        public ManualResetEvent Event = new ManualResetEvent(false);

        #region ILogReceiver Members

        /// <summary>
        /// Received a new log entry.
        /// </summary>
        /// <param name="dto">Log entry</param>
        public void ReceivedLogEntry(LogEntryDTO dto)
        {
            Entries.Add(dto);
            Event.Set();
        }

        #endregion
    }
}