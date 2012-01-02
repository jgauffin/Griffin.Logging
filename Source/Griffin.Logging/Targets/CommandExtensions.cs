using System;
using System.Data;
using System.Diagnostics.Contracts;

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
			Contract.Requires<ArgumentNullException>(command != null);
			Contract.Requires(!String.IsNullOrEmpty(name));
			Contract.Ensures(Contract.Result<IDataParameter>() != null);

			var p = command.CreateParameter();
			p.ParameterName = name;
			p.Value = value;
			command.Parameters.Add(p);
			return p;
		}
	}
}