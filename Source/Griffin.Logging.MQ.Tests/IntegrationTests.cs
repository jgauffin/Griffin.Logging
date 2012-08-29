using System;
using System.Linq;
using System.Messaging;
using System.Reflection;
using System.Threading;
using Xunit;

namespace Griffin.Logging.MQ.Tests
{
    public class IntegrationTests
    {
   
        public IntegrationTests()
        {
            Queue.Reset();
        }

        [Fact]
        public void Send()
        {
            SimpleLogManager.Instance.AddMessageQueue("MyApp", Queue.Name);
            var logger = SimpleLogManager.Instance.GetLogger(GetType());
            logger.Warning("Hello world");

            var queue = new MessageQueue(Queue.Name) {Formatter = new XmlMessageFormatter(new[] {typeof (LogEntryDTO)})};
            var msg = queue.Receive(TimeSpan.FromSeconds(10));

            Assert.NotNull(msg);
            Assert.NotNull(msg.Body);
            var dto = (LogEntryDTO) msg.Body;
            Assert.Equal("MyApp", dto.ApplicationName);
            Assert.Equal(null, dto.TargetName);
            Assert.Equal(MethodBase.GetCurrentMethod().Name, dto.LoggingMethod);
            Assert.Equal("Warning", dto.LogLevel.ToString());
            Assert.Equal(Thread.CurrentThread.ManagedThreadId, dto.ThreadId);
            Assert.Equal("Hello world", dto.Message);
        }

        [Fact]
        public void Receive()
        {
            var dto = new LogEntryDTO("MyApp", "SomeTarget", new LogEntry
                {
                    CreatedAt = DateTime.Now,
                    Exception = new Exception("Something awful"),
                    LoggedType = typeof (LogEntryDTO),
                    MethodName = "Some",
                    LogLevel = LogLevel.Info,
                    Message = "No no",
                    ThreadId = 20,
                    UserName = "Arnwe"
                });
            var queue = new MessageQueue(Queue.Name) {Formatter = new XmlMessageFormatter(new[] {typeof (LogEntryDTO)})};
            queue.Send(new Message(dto));

            var receiver = new Receiver();
            var listenr = new QueueReader(Queue.Name, receiver);
            receiver.Event.WaitOne(1000);

            Assert.NotEmpty(receiver.Entries);
            var entry = receiver.Entries.First();
            Assert.Equal("Arnwe", entry.UserName);
            Assert.Equal("No no", entry.Message);
        }
    }
}