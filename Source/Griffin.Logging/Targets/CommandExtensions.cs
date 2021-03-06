using System;
using System.Data;

namespace Griffin.Logging.Targets
{
    /// <summary>
    /// Extensions making it easier to work with ADO.NET commands.
    /// </summary>
    /// <remarks>
    /// This extension do also exist in <c>Griffin.Core</c>, but since the logging framework should have no dependencies
    /// we copied it here too.
    /// </remarks>
    public static class CommandExtensions
    {
        /// <summary>
        /// Add a parameter to a ADO.NET command
        /// </summary>
        /// <param name="command">Command</param>
        /// <param name="name">Parameter name</param>
        /// <param name="value">Value</param>
        /// <returns>Created parameter</returns>
        public static IDataParameter AddParameter(this IDbCommand command, string name, object value)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (name == null) throw new ArgumentNullException("name");

            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            command.Parameters.Add(p);
            return p;
        }
    }
}