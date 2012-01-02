using System;
using System.Diagnostics.Contracts;

namespace Griffin.Logging.Targets
{
	/// <summary>
	/// Extensions for <see cref="LogEntry"/>
	/// </summary>
	public static class LogEntryExtensions
	{
		/// <summary>
		/// Build source using either stackframes or the logged type
		/// </summary>
		/// <param name="entry">Entry containing information</param>
		/// <returns>Formatted source</returns>
		/// <remarks>
		/// Used to switch between <see cref="LogEntry.StackFrames"/> or <see cref="LogEntry.LoggedType"/> depending
		/// on if the stackframes are collected or not.
		/// </remarks>
		public static string StackFrameOrType(this LogEntry entry)
		{
			Contract.Requires<ArgumentNullException>(entry != null);
			Contract.Ensures(!String.IsNullOrEmpty(Contract.Result<string>()));

			if (entry.StackFrames == null)
				return entry.LoggedType.Name;

			return entry.StackFrames[0].GetMethod().ReflectedType.Name + "."
			       + entry.StackFrames[0].GetMethod().Name;
		}
	}
}