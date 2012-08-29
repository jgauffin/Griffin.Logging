using Griffin.Logging.Filters;

namespace Griffin.Logging.Tests.Targets
{
    public class TestPostFilter : IPostFilter
    {
        public bool CanLogResult { get; set; }

        #region IPostFilter Members

        public bool CanLog(LogEntry entry)
        {
            return CanLogResult;
        }

        #endregion
    }
}