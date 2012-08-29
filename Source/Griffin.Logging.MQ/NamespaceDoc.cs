using System.Runtime.CompilerServices;

namespace Griffin.Logging.MQ
{
    /// <summary>
    /// This target allows you to send all log entries over a Microsoft MQ channel to somewhere.
    /// </summary>
    /// <remarks>
    /// <para>The simple log manager got a new extension method which you can use to configure this target.
    /// <example>
    /// <code>
    /// SimpleLogManager.AddMessageQueue("MyApp", "MyApp.Core", "/path/to/queue");
    /// </code>
    /// </example>
    /// </para>
    /// <para>The fluent syntax also got a new target extension.
    /// <example>
    /// <code>
    /// new FluentConfiguration()
    /// 	.LogNamespace("Griffin.Logging")
    /// 		.AndChildNamespaces
    /// 		.ToTargetNamed("Queue")
    /// 	.AddTarget("Queue").As
    /// 		.MessageQueue("LoggingApp", "Tests", Queue.Name)
    /// 		.Done
    /// 	.Build();
    /// 
    /// </code>
    /// </example>
    /// </para>
    /// </remarks>
    [CompilerGenerated]
    internal class NamespaceDoc
    {
    }
}