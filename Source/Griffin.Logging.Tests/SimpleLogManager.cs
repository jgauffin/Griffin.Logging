using System;
using Xunit;

namespace Griffin.Logging.Tests
{
    public class SimpleLogManagerTest
    {
        private readonly ILogger _logger;

        public SimpleLogManagerTest()
        {
            // true will create a console if it do not exist.
            // perfect for debugging winfoms/wfp applications.
            SimpleLogManager.AddConsole(true);

            // Will create a file log
            SimpleLogManager.AddFile("Mylog");

            // and assign a logger.
            _logger = LogManager.GetLogger<SimpleLogManagerTest>();
        }

        [Fact]
        public void SomeMethod()
        {
            // Logging isn't harder than this.
            // one method per log level
            _logger.Debug("We are in some method");

            int i = 0;
            try
            {
                int a = 5/i;
            }
            catch (Exception err)
            {
                // Exceptions are automatically formatted properly
                // including all nested inner exceptions.
                _logger.Error("Oooops, something went wrong!", err);
            }
        }
    }
}