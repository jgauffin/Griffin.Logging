using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Griffin.Logging.Targets;
using Griffin.TestTools.Data;
using Xunit;

namespace Griffin.Logging.Tests.Targets
{
    public class AdoNetTargetTest
    {
        [Fact]
        public void TestSave()
        {
			
            FakeDbProviderFactory.Setup();

			var _configurationString = ConfigurationManager.ConnectionStrings["FakeDb"];
			var _providerFactory = DbProviderFactories.GetFactory(_configurationString.ProviderName);


            AdoNetTarget target = new AdoNetTarget("FakeDb");
            var logEntry = new LogEntry
                               {
                                   CreatedAt = DateTime.Now,
                                   LoggedType = GetType(),
                                   LogLevel = LogLevel.Warning,
                                   Message = "Hejsan",
                                   StackFrames = new StackTrace().GetFrames(),
                                   ThreadId = Thread.CurrentThread.ManagedThreadId,
                                   UserName = Thread.CurrentPrincipal.Identity.Name
                               };
            target.Enqueue(logEntry);

            var con = FakeDbProviderFactory.Instance.CurrentConnection;

            var sql = con.Commands.First().CommandStrings.First();
            var parameters = con.Commands.First().ExecutedParameterCollections.First();
            var expectedSql =
                "INSERT INTO LogEntries (UserName, CreatedAt, Source, Message, Exception, ThreadId, LogLevel) VALUES(@user, @createdAt, @source, @message, @exception, @threadId, @logLevel)";

            Assert.Equal(expectedSql, sql);
            Assert.Equal(logEntry.UserName,  parameters[0].Value);
            Assert.Equal(logEntry.CreatedAt, parameters[1].Value);
            Assert.Equal("AdoNetTargetTest.TestSave", parameters[2].Value);
            Assert.Equal(logEntry.Message, parameters[3].Value);
            Assert.Equal(null, parameters[4].Value);
            Assert.Equal(Thread.CurrentThread.ManagedThreadId, parameters[5].Value);
            Assert.Equal((int)logEntry.LogLevel, parameters[6].Value);
        }
    }
}
