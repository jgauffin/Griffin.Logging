using System;
using System.Runtime.Serialization;

namespace Griffin.Logging.Net
{
    /// <summary>
    /// Used to transfer the log entry over any network
    /// </summary>
    [Serializable, DataContract]
    public class LogEntryDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntryDTO"/> class.
        /// </summary>
        public LogEntryDTO()
        {
            ComputerName = Environment.MachineName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntryDTO"/> class.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="target">The target (i.e. module in the application).</param>
        /// <param name="entry">The entry.</param>
        public LogEntryDTO(string applicationName, string target, LogEntry entry)
        {
            if (applicationName == null) throw new ArgumentNullException("applicationName");
            if (entry == null) throw new ArgumentNullException("entry");

            ApplicationName = applicationName;
            TargetName = target;
            UserName = entry.UserName;
            CreatedAt = entry.CreatedAt;
            Exception = entry.Exception == null ? null : new ExceptionDTO(entry.Exception);
            LogLevel = entry.LogLevel;
            if (entry.LoggedType != null)
                LoggingType = entry.LoggedType.FullName;
            Message = entry.Message;
            LoggingMethod = entry.MethodName;
            ComputerName = Environment.MachineName;
            ThreadId = entry.ThreadId;
        }

        /// <summary>
        /// Gets or sets computer that the logs are from.
        /// </summary>
        [DataMember(Order = 1)]
        public string ComputerName { get; set; }

        /// <summary>
        /// Gets name of the application which is logged.
        /// </summary>
        [DataMember(Order = 2)]
        public string ApplicationName { get; set; }

        /// <summary>
        /// Gets or sets target (i.e. module in the application)
        /// </summary>
        [DataMember(Order = 3)]
        public string TargetName { get; set; }


        /// <summary>
        /// Gets or sets name of the current identity.
        /// </summary>
        [DataMember(Order = 4)]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets when entry was created
        /// </summary>
        [DataMember(Order = 5)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the type that is logging (<c>Type.FullName</c>)
        /// </summary>
        [DataMember(Order = 6)]
        public string LoggingType { get; set; }

        /// <summary>
        /// Gets or sets <c>StackFrame.ToString()</c> for the caller
        /// </summary>
        [DataMember(Order = 7)]
        public string LoggingMethod { get; set; }

        /// <summary>
        /// Gets or sets log message.
        /// </summary>
        [DataMember(Order = 8)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets exception (optional)
        /// </summary>
        [DataMember(Order = 9)]
        public ExceptionDTO Exception { get; set; }

        /// <summary>
        /// Gets or sets id of current thread.
        /// </summary>
        [DataMember(Order = 10)]
        public int ThreadId { get; set; }

        /// <summary>
        /// Gets or sets how important the log entry is
        /// </summary>
        /// <remarks>
        /// Here is our recommendation to how you should use each log level.
        /// <list type="table">
        /// <item>
        /// <term>Debug</term>
        /// <description>Debug entries are usually used only when debugging. They can be used to track
        /// variables or method contracts. There might be several debug entries per method.</description>
        /// </item>
        /// <item>
        /// <term>Info</term>
        /// <description>Informational messages are used to track state changes such as login, logout, record updates etc. 
        /// There are at most one entry per method.</description>
        /// </item>
        /// <item>
        /// <term>Warning</term>
        /// <description>
        /// Warnings are used when something unexpected happend but the application can handle it and continue as expected.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Error</term>
        /// <description>
        /// Errors are when something unexpected happens and the application cannot deliver result as expected. It might or might not
        /// mean that the application has to be restarted.
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        [DataMember(Order = 11)]
        public LogLevel LogLevel { get; set; }
    }
}