using System;
using System.Reflection;
using System.Threading;
using Griffin.Logging.Targets.File;
using Xunit;

namespace Griffin.Logging.Tests.Targets
{
    public class FileTargetTest : IFileWriter
    {
        private readonly FileTarget _target;
        private string _writtenMessage;

        public FileTargetTest()
        {
            _target = new FileTarget(this);
        }

        #region IFileWriter Members

        public FileConfiguration Configuration
        {
            get
            {
                return new FileConfiguration
                    {
                        CreateDateFolder = true,
                        Path = @"C:\Temp\",
                        DaysToKeep = 1
                    };
            }
        }

        public string Name
        {
            get { return "TestLog"; }
        }

        public void Write(string logEntry)
        {
            _writtenMessage = logEntry;
        }

        #endregion

        [Fact]
        public void TestEntry()
        {
            _target.Enqueue(new LogEntry
                {
                    CreatedAt = DateTime.Now,
                    LogLevel = LogLevel.Warning,
                    LoggedType = GetType(),
                    Message = "Hello world",
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = Environment.UserName,
                    MethodName = MethodBase.GetCurrentMethod().Name
                });

            //0						  1       2 3        4                                    5
            //2012-01-02 13:01:41.050|Warning|6|xgauffin|RuntimeMethodHandle.InvokeMethodFast|Hello world
            var actual = _writtenMessage.Split('|');
            Assert.Equal("Warning", actual[1]);
            Assert.Equal("FileTargetTest.TestEntry", actual[4]);
            Assert.Equal("Hello world\r\n", actual[5]);
        }
    }
}