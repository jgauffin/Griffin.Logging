using Griffin.Logging.MQ;

namespace Griffin.Logging
{
    /// <summary>
    /// Extension used to MSMQ
    /// </summary>
    public static class FluentExtensions
    {
        /// <summary>
        /// Adds a database logger.
        /// </summary>
        /// <param name="instance">fluent configuration instance.</param>
        /// <param name="appName">Application name (used to identify the messages from the current application) </param>
        /// <param name="module">Optional. Namespace/project etc. Used to identify the logging classes </param>
        /// <param name="queueName">Message queue name (read MSDN for the syntax)</param>
        /// <returns>Fluent configuration instance</returns>
        /// <remarks>
        /// The message queue must have been created before you try to use it.
        /// </remarks>
        public static FluentTargetConfiguration MessageQueue(this FluentTargetConfigurationTypes instance,
                                                             string appName, string module, string queueName)
        {
            instance.Add(new MqTarget(appName, module, queueName));
            return instance.Done;
        }
    }
}