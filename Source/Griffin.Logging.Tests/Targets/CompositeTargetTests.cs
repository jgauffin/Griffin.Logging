using System.Linq;
using Griffin.Logging.Targets;
using Xunit;

namespace Griffin.Logging.Tests.Targets
{
    public class CompositeTargetTests
    {
        [Fact]
        public void TestWithFilterOn()
        {
            var target = new CompositeTarget("MyName");
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
            var target = new CompositeTarget("MyName");
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
            var target = new CompositeTarget("MyName");
            target.Targets.Add(new TestTarget());
            target.Targets.Add(new TestTarget());

            target.Enqueue(new LogEntry());

            Assert.NotEmpty(((TestTarget) target.Targets.First()).Entries);
            Assert.NotEmpty(((TestTarget) target.Targets.Last()).Entries);
        }

        [Fact]
        public void TestWithTwoTargetsAndActiveFilter()
        {
            var target = new CompositeTarget("MyName");
            target.Targets.Add(new TestTarget());
            target.Targets.Add(new TestTarget());
            var filter = new TestPostFilter {CanLogResult = false};
            target.AddFilter(filter);

            target.Enqueue(new LogEntry());

            Assert.Empty(((TestTarget) target.Targets.First()).Entries);
            Assert.Empty(((TestTarget) target.Targets.Last()).Entries);
        }
    }
}