using System.Collections.Generic;
using System.Linq;
using Griffin.Logging.Filters;
using Griffin.Logging.Targets;

namespace Griffin.Logging.Tests.Targets
{
    public class TestTarget : ILogTarget
    {
        private readonly List<IPostFilter> _filters = new List<IPostFilter>();
        private List<LogEntry> _entries = new List<LogEntry>();

        public string Name { get; set; }

        public List<IPostFilter> Filters
        {
            get { return _filters; }
        }

        public List<LogEntry> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public void AddFilter(IPostFilter filter)
        {
            Filters.Add(filter);
        }

        public void Enqueue(LogEntry entry)
        {
            if (Filters.Any(f => !f.CanLog(entry)))
                return;

            Entries.Add(entry);
        }
    }
}