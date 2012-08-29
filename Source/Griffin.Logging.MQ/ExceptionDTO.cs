using System;

namespace Griffin.Logging.MQ
{
    /// <summary>
    /// DTO for exceptions
    /// </summary>
    [Serializable]
    public class ExceptionDTO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDTO"/> class.
        /// </summary>
        public ExceptionDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionDTO"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public ExceptionDTO(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            Message = exception.Message;
            StackTrace = exception.StackTrace;
            ExceptionName = exception.GetType().Name;
            ExceptionNamespace = exception.GetType().Namespace;
        }

        /// <summary>
        /// Gets or sets exception message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets stack trace
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets exception name (type name)
        /// </summary>
        public string ExceptionName { get; set; }


        /// <summary>
        /// Gets or sets namespace (that the exception type is in)
        /// </summary>
        public string ExceptionNamespace { get; set; }
    }
}