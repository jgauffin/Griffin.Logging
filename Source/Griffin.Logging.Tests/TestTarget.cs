using System.Collections.Generic;
using Griffin.Logging.Filters;
using Griffin.Logging.Targets;

namespace Griffin.Logging.Tests
{
    public class TestTarget : ILogTarget
    {
        public List<LogEntry> LogEntries = new List<LogEntry>();

        public TestTarget()
        {
            Name = "TestTarget";
        }

        #region ILogTarget Members

        /// <summary>
        /// Gets name of target. 
        /// </summary>
        /// <remarks>
        /// It must be unique for each target. The filename works for file targets etc.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Add a filter for this target.
        /// </summary>
        /// <param name="filter">Filters are used to validate if an entry can be written to a target or not.</param>
        public void AddFilter(IPostFilter filter)
        {
        }

        /// <summary>
        /// Enqueue to be written
        /// </summary>
        /// <param name="entry"></param>
        /// <remarks>
        /// The entry might be written directly in the same thread or enqueued to be written
        /// later. It's up to each implementation to decide. Keep in mind that a logger should not
        /// introduce delays in the thread execution. If it's possible that it will delay the thread,
        /// enqueue entries instead and write them in a seperate thread.
        /// </remarks>
        public void Enqueue(LogEntry entry)
        {
            LogEntries.Add(entry);
        }

        #endregion
    }
}