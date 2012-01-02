using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Griffin.Logging.Filters;
using Xunit;

namespace Griffin.Logging.Tests.Filters
{
    public class NamespaceFilterTest
    {
        [Fact]
        public void TestOtherNamespaceWithoutChildren()
        {
            var filter = new NamespaceFilter("Bajs", false);
            Assert.False(filter.CanLog(GetType(), LogLevel.Debug));
        }

        [Fact]
        public void TestOtherNamespaceWithChildren()
        {
            var filter = new NamespaceFilter("Bajs", true);
            Assert.False(filter.CanLog(GetType(), LogLevel.Debug));
        }

        [Fact]
        public void TestParentNamespaceWithoutChildren()
        {
            var filter = new NamespaceFilter("Griffin.Logging.Tests", false);
            Assert.False(filter.CanLog(GetType(), LogLevel.Debug));
            
        }

        [Fact]
        public void TestParentNamespaceWithChildren()
        {
            var filter = new NamespaceFilter("Griffin.Logging.Tests", true);
            Assert.True(filter.CanLog(GetType(), LogLevel.Debug));

        }

        [Fact]
        public void TestThisNamespaceWithoutChildren()
        {
            var filter = new NamespaceFilter("Griffin.Logging.Tests.Filters", false);
            Assert.True(filter.CanLog(GetType(), LogLevel.Debug));

        }

        [Fact]
        public void TestThisNamespaceWithChildren()
        {
            var filter = new NamespaceFilter("Griffin.Logging.Tests.Filters", true);
            Assert.True(filter.CanLog(GetType(), LogLevel.Debug));

        }

    }
}
