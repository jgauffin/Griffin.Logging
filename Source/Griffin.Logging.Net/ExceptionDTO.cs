using System;
using System.Runtime.Serialization;

namespace Griffin.Logging.Net
{
    /// <summary>
    /// DTO for exceptions
    /// </summary>
    [Serializable, DataContract]
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
        [DataMember(Order = 1)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets stack trace
        /// </summary>
        [DataMember(Order = 2)]
        public string StackTrace { get; set; }

        /// <summary>
        /// Gets or sets exception name (type name)
        /// </summary>
        [DataMember(Order = 3)]
        public string ExceptionName { get; set; }


        /// <summary>
        /// Gets or sets namespace (that the exception type is in)
        /// </summary>
        [DataMember(Order = 4)]
        public string ExceptionNamespace { get; set; }
    }
}