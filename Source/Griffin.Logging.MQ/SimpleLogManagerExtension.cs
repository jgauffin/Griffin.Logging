using System;
using Griffin.Logging.MQ;

namespace Griffin.Logging
{
    /// <summary>
    /// Extension method for the simple log manager
    /// </summary>
    public static class SimpleLogManagerExtension
    {
        /// <summary>
        /// Add a message queue
        /// </summary>
        /// <param name="instance">Log manager instance</param>
        /// <param name="appName">Application name (to be able to identify this app at the receiver end)</param>
        /// <param name="module">Part of the application, for instance a specific class library or namespace</param>
        /// <param name="queueName">Queue which is used for the transportation.</param>
        public static void AddMessageQueue(this SimpleLogManager instance, string appName, string module,
                                           string queueName)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (appName == null) throw new ArgumentNullException("appName");
            if (module == null) throw new ArgumentNullException("module");
            if (queueName == null) throw new ArgumentNullException("queueName");
            SimpleLogManager.Instance.AddTarget(new MqTarget(appName, module, queueName));
        }

        /// <summary>
        /// Add a message queue
        /// </summary>
        /// <param name="instance">Log manager instance</param>
        /// <param name="appName">Application name (to be able to identify this app at the receiver end)</param>
        /// <param name="queueName">Queue which is used for the transportation.</param>
        public static void AddMessageQueue(this SimpleLogManager instance, string appName, string queueName)
        {
            if (instance == null) throw new ArgumentNullException("instance");
            if (appName == null) throw new ArgumentNullException("appName");
            if (queueName == null) throw new ArgumentNullException("queueName");
            SimpleLogManager.Instance.AddTarget(new MqTarget(appName, null, queueName));
        }
    }
}