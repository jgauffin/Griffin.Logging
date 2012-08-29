using System.Linq;
using System.Reflection;
using Xunit;

namespace Griffin.Logging.Tests
{
    public class LogEntryTests
    {
        [Fact]
        public void CheckMethodName()
        {
            var target = new TestTarget();
            var logger = new Logger(GetType(), new[] {target});
            logger.Warning("Nono!");

            var entry = target.LogEntries.FirstOrDefault();
            Assert.NotNull(entry);
            Assert.Equal(MethodBase.GetCurrentMethod().Name, entry.MethodName);

        }
    }
}