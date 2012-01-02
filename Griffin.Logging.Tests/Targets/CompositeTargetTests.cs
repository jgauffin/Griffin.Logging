using System;
using System.Linq;
using System.Text;
using Griffin.Logging.Targets;
using Xunit;

namespace Griffin.Logging.Tests.Targets
{
    public class CompositeTargetTests
    {
        [Fact]
        public void TestWithFilterOn()
        {
            CompositeTarget target = new CompositeTarget("MyName");
            var testTarget = new TestTarget();
            target.Targets.Add(testTarget);
            var filter = new TestPostFilter {CanLogResult = false};
            target.AddFilter(filter);
            
            target.Enqueue(new LogEntry());

            Assert.Empty(testTarget.Entries);
        }

        [Fact]
        public void TestWithFilterOff()
        {
            CompositeTarget target = new CompositeTarget("MyName");
            var testTarget = new TestTarget();
            target.Targets.Add(testTarget);
            var filter = new TestPostFilter {CanLogResult = true};
            target.AddFilter(filter);

            target.Enqueue(new LogEntry());

            Assert.NotEmpty(testTarget.Entries);
        }

        [Fact]
        public void TestWithTwoTargets()
        {
            CompositeTarget target = new CompositeTarget("MyName");
            target.Targets.Add(new TestTarget());
            target.Targets.Add(new TestTarget());

            target.Enqueue(new LogEntry());

            Assert.NotEmpty(((TestTarget)target.Targets.First()).Entries);
            Assert.NotEmpty(((TestTarget)target.Targets.Last()).Entries);
        }

        [Fact]
        public void TestWithTwoTargetsAndActiveFilter()
        {
            CompositeTarget target = new CompositeTarget("MyName");
            target.Targets.Add(new TestTarget());
            target.Targets.Add(new TestTarget());
            var filter = new TestPostFilter { CanLogResult = false };
            target.AddFilter(filter);

            target.Enqueue(new LogEntry());

            Assert.Empty(((TestTarget)target.Targets.First()).Entries);
            Assert.Empty(((TestTarget)target.Targets.Last()).Entries);
        }

    }
}
