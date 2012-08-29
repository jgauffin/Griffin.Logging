using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using Griffin.Logging.Net;
using Xunit;

namespace Griffin.Logging.MQ.Tests
{
    
    public class ConfigurationTests
    {
        [Fact]
        public void Fluent()
        {
            Queue.Reset();

            new FluentConfiguration()
                .LogNamespace("Griffin.Logging")
                    .AndChildNamespaces
                    .ToTargetNamed("Queue")
                .AddTarget("Queue").As
                    .MessageQueue("LoggingApp", "Tests", Queue.Name)
                    .Done
                .Build();

            var logger = LogManager.GetLogger<ConfigurationTests>();
            logger.Warning("Hello");

            var queue = new MessageQueue(Queue.Name) { Formatter = new XmlMessageFormatter(new[] { typeof(LogEntryDTO) }) };
            var msg = queue.Receive(TimeSpan.FromSeconds(10));
            Assert.NotNull(msg);
            Assert.NotNull(msg.Body);
        }

        [Fact]
        public void Simple()
        {
            Queue.Reset();

            SimpleLogManager.Instance.AddMessageQueue("MyApp", Queue.Name);

            var logger = LogManager.GetLogger<ConfigurationTests>();
            logger.Warning("Hello");

            var queue = new MessageQueue(Queue.Name) { Formatter = new XmlMessageFormatter(new[] { typeof(LogEntryDTO) }) };
            var msg = queue.Receive(TimeSpan.FromSeconds(10));
            Assert.NotNull(msg);
            Assert.NotNull(msg.Body);
        }

    }
}
