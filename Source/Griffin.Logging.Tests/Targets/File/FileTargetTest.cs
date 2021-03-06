﻿using System;
using System.Reflection;
using System.Threading;
using Griffin.Logging.Targets.File;
using Xunit;

namespace Griffin.Logging.Tests.Targets.File
{
    public class FileTargetTest : FileTarget
    {
        private string _exception;
        private string _logEntry;
        private string _message;
        private string _stacktrace;
        private string _userName;

        public FileTargetTest() : base(TestWriter.Instance)
        {
        }

        [Fact]
        public void TestLongUserName()
        {
            var entry = new LogEntry
                {
                    CreatedAt = DateTime.Now,
                    LoggedType = GetType(),
                    LogLevel = LogLevel.Error,
                    Message = "Hej\r\nTvå rader",
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = "OmgLongUserNameWithATwistAndSomeMoreCharacters",
                    MethodName = MethodBase.GetCurrentMethod().Name
                };
            Enqueue(entry);
            Assert.Equal("OmgLongUserNameWithATwistAndSomeMoreCha.", _userName);
        }

        [Fact]
        public void TestLongUserNameWithDomain()
        {
            var entry = new LogEntry
                {
                    CreatedAt = DateTime.Now,
                    LoggedType = GetType(),
                    LogLevel = LogLevel.Error,
                    Message = "Hej\r\nTvå rader",
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = "DomainName\\OmgLongUserNameWithATwistAndSomeMore",
                    MethodName = MethodBase.GetCurrentMethod().Name
                };
            Enqueue(entry);
            Assert.Equal("OmgLongUserNameWithATwistAndSomeMore", _userName);
        }

        [Fact]
        public void TestUserName()
        {
            var entry = new LogEntry
                {
                    CreatedAt = DateTime.Now,
                    LoggedType = GetType(),
                    LogLevel = LogLevel.Error,
                    Message = "Hej\r\nTvå rader",
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = "OmgLongUserNameWithATwistAndSomeMore",
                    MethodName = MethodBase.GetCurrentMethod().Name
                };
            Enqueue(entry);
            Assert.Equal("OmgLongUserNameWithATwistAndSomeMore", _userName);
        }

        [Fact]
        public void TestMultiRowMessage()
        {
            var entry = new LogEntry
                {
                    CreatedAt = DateTime.Now,
                    LoggedType = GetType(),
                    LogLevel = LogLevel.Error,
                    Message = "Hej\r\nTvå rader",
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = "OmgLongUserNameWithATwistAndSomeMore",
                    MethodName = MethodBase.GetCurrentMethod().Name,
                };
            Enqueue(entry);
            Assert.Equal("Hej\r\n\tTvå rader", _message);
        }


        protected override string FormatException(Exception exception, int intendation)
        {
            _exception = base.FormatException(exception, intendation);
            return _exception;
        }

        protected override string FormatLogEntry(LogEntry entry)
        {
            _logEntry = base.FormatLogEntry(entry);
            return _logEntry;
        }

        protected override string FormatCallingMethod(Type loggingType, string callingMethod, int maxSize)
        {
            _stacktrace = base.FormatCallingMethod(loggingType, callingMethod, maxSize);
            return _stacktrace;
        }

        protected override string FormatUserName(string userName, int maxSize)
        {
            _userName = base.FormatUserName(userName, maxSize);
            return _userName;
        }

        protected override string FormatMessage(string message)
        {
            _message = base.FormatMessage(message);
            return _message;
        }
    }
}