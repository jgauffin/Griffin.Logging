/*
 * Copyright (c) 2011, Jonas Gauffin. All rights reserved.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston,
 * MA 02110-1301 USA
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Griffin.Logging.Filters;
using Griffin.Logging.Targets;

namespace Griffin.Logging
{
    /// <summary>
    /// Default implementation of an logger.
    /// </summary>
    /// <remarks>
    /// This implementation will assemble all information into a <see cref="LogEntry"/> class, including stacktrace, and 
    /// then check each filter to see if it can log. Then it will iterate each target and write the entry to them.
    /// </remarks>
    public class Logger : ILogger
    {
        private readonly IEnumerable<IPreFilter> _filters;
        private readonly Type _loggedType;
        private readonly IEnumerable<ILogTarget> _targets;

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="loggedType">Type that requested the logger.</param>
        /// <param name="filters">The filters.</param>
        /// <param name="targets">The targets.</param>
        public Logger(Type loggedType, IEnumerable<IPreFilter> filters, IEnumerable<ILogTarget> targets)
        {
            _loggedType = loggedType;
            _filters = filters;
            _targets = targets;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="loggedType">Type that requested the logger.</param>
        /// <param name="targets">The targets.</param>
        public Logger(Type loggedType, IEnumerable<ILogTarget> targets)
        {
            _loggedType = loggedType;
            _targets = targets;
            _filters = new List<IPreFilter>();
        }

        /// <summary>
        /// Gets all filters
        /// </summary>
        internal IEnumerable<IPreFilter> Filters
        {
            get { return _filters; }
        }

        /// <summary>
        /// Gets all logging targets
        /// </summary>
        internal IEnumerable<ILogTarget> Targets
        {
            get { return _targets; }
        }

        #region ILogger Members

        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        public void Debug(string message)
        {
            WriteEntry(LogLevel.Debug, message, null);
        }

        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        public void Debug(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Debug, string.Format(message, formatters), null);
        }

        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        public void Debug(string message, Exception exception)
        {
            WriteEntry(LogLevel.Debug, message, exception);
        }

        /// <summary>
        /// Write a diagnostic message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Log entries which is helpful during debugging. The amount of debug entries can be vast,
        /// and it's therefore not recommended to have them turned on in production systems unless 
        /// it's really required.
        /// </remarks>
        public void Debug(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Debug, string.Format(message, formatters), exception);
        }

        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        public void Error(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Error, string.Format(message, formatters), exception);
        }

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        public void Info(string message)
        {
            WriteEntry(LogLevel.Info, message, null);
        }

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        public void Info(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Info, string.Format(message, formatters), null);
        }

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        public void Info(string message, Exception exception)
        {
            WriteEntry(LogLevel.Info, message, exception);
        }

        /// <summary>
        /// Write an informational message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Informational messages are more important than Debug messages and are usually state changes like
        /// a user logs on, sends an email etc. There are typically at most one <c>Info</c> message per method.
        /// </remarks>
        public void Info(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Info, string.Format(message, formatters), null);
        }

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        public void Warning(string message)
        {
            WriteEntry(LogLevel.Warning, message, null);
        }

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        public void Warning(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Warning, string.Format(message, formatters), null);
        }

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        public void Warning(string message, Exception exception)
        {
            WriteEntry(LogLevel.Warning, message, exception);
        }

        /// <summary>
        /// Write an warning message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Warnings should be written when something did not go as planned, but your application can recover from it
        /// and continue as expected.
        /// </remarks>
        public void Warning(string message, Exception exception, params object[] formatters)
        {
            WriteEntry(LogLevel.Warning, string.Format(message, formatters), exception);
        }

        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        public void Error(string message)
        {
            WriteEntry(LogLevel.Error, message, null);
        }

        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="formatters">Formatters used in the log message</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        public void Error(string message, params object[] formatters)
        {
            WriteEntry(LogLevel.Error, string.Format(message, formatters), null);
        }


        /// <summary>
        /// Write an error message.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Exception thrown by code. All inner exceptions will automatically be logged.</param>
        /// <remarks>
        /// Use this method to log errors in your code that prevent the application from continuing as expected,
        /// but the error is not severe enough to shut down the system.
        /// </remarks>
        public void Error(string message, Exception exception)
        {
            WriteEntry(LogLevel.Error, message, exception);
        }

        #endregion

        private void WriteEntry(LogLevel level, string message, Exception exception)
        {
            // find any filter that says no to logging
            if (_filters.Any(filter => !filter.CanLog(_loggedType, level)))
            {
                Console.WriteLine("Filter blocks");
                return;
            }


            var frame = new StackFrame(2);

            var userName = Thread.CurrentPrincipal.Identity.Name;
            if (string.IsNullOrEmpty(userName))
                userName = Environment.UserName;

            var entry = new LogEntry
                {
                    CreatedAt = DateTime.Now,
                    Exception = exception,
                    LogLevel = level,
                    LoggedType = _loggedType,
                    Message = message,
                    MethodName = frame.GetMethod().Name,
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    UserName = userName
                };

            foreach (var target in _targets)
            {
                target.Enqueue(entry);
            }
        }
    }
}