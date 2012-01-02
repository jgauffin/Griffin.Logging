using Griffin.Logging.Filters;

namespace Griffin.Logging.Tests.Targets
{
    public class TestPostFilter : IPostFilter
    {
        public bool CanLog(LogEntry entry)
        {
            return CanLogResult;
        }

        public bool CanLogResult { get; set; }
    }
}