using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Logging.Filters;
using Xunit;

namespace Griffin.Logging.Tests.Filters
{
    public class LevelFilterTest
    {
        [Fact]
        public void TestInRangeLowerLimit()
        {
            var filter = new LevelFilter(LogLevel.Info, LogLevel.Warning);
            var actual = filter.CanLog(new LogEntry {LogLevel = LogLevel.Info});
            Assert.True(actual);
        }
        
        [Fact]
        public void TestInRange()
        {
            var filter = new LevelFilter(LogLevel.Debug, LogLevel.Warning);
            var actual = filter.CanLog(new LogEntry { LogLevel = LogLevel.Info });
            Assert.True(actual);
        }

        [Fact]
        public void TestInRangeUpperLimit()
        {
            var filter = new LevelFilter(LogLevel.Info, LogLevel.Warning);
            var actual = filter.CanLog(new LogEntry { LogLevel = LogLevel.Warning });
            Assert.True(actual);
        }

        [Fact]
        public void TestLessThanLowerLimit()
        {
            var filter = new LevelFilter(LogLevel.Info, LogLevel.Warning);
            var actual = filter.CanLog(new LogEntry { LogLevel = LogLevel.Debug });
            Assert.False(actual);
        }

        [Fact]
        public void TestMoreThanUpperLimit()
        {
            var filter = new LevelFilter(LogLevel.Info, LogLevel.Warning);
            var actual = filter.CanLog(new LogEntry { LogLevel = LogLevel.Error });
            Assert.False(actual);
        }
    }
}
