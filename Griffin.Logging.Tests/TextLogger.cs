using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Griffin.Logging.Filters;
using Griffin.Logging.Targets.File;
using Xunit;

namespace Griffin.Logging.Tests
{
    public class TextLoggerTest
    {
        [Fact]
        public void TestLogging()
        {
            var config = new FileConfiguration
                             {
                                 CreateDateFolder = true,
                                 DaysToKeep = 3,
                                 Path = AppDomain.CurrentDomain.BaseDirectory
                             };

            var target = new FileTarget("Everything", config);

            target.Enqueue(new LogEntry
                               {
                                   CreatedAt = DateTime.Now,
                                   LogLevel = LogLevel.Debug,
                                   Message = "Hello world",
                                   StackFrames = new StackTrace().GetFrames(),
                                   ThreadId = Thread.CurrentThread.ManagedThreadId,
                                   UserName = Thread.CurrentPrincipal.Identity.Name
                                              ?? Environment.UserName
                               });

            var target2 = new PaddedFileTarget("EVeryone", config);
            target2.Enqueue(new LogEntry
                                {
                                    CreatedAt = DateTime.Now,
                                    LogLevel = LogLevel.Debug,
                                    Message = "Hello world",
                                    StackFrames = new StackTrace().GetFrames(),
                                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                                    UserName = Thread.CurrentPrincipal.Identity.Name
                                               ?? Environment.UserName
                                });

            var logger = new Logger(GetType(), new[] {target2, target});
            logger.Info("Hello");
            logger.Debug("Hello");
            logger.Warning("Hello");
            logger.Error("Hello");
        }
    }
}