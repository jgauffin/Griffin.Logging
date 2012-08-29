using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using Griffin.Logging.Filters;
using Griffin.Logging.Net;
using Griffin.Logging.Targets;

namespace Griffin.Logging.MQ
{
    /// <summary>
    /// Send log messages over MQ.
    /// </summary>
    /// <remarks>
    /// <para>You have to create the Message Queue by yourself.</para>
    /// <para>Messages are sent without using transactions or the recoverable flag to get best performance.</para>
    /// </remarks>
    public class MqTarget : ILogTarget
    {
        private readonly string _applicationName;
        private readonly LinkedList<IPostFilter> _filters = new LinkedList<IPostFilter>();
        private readonly string _module;
        private readonly MessageQueue _queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MqTarget"/> class.
        /// </summary>
        /// <param name="module">Module/Subsection/project in the application ( to aid you during debugging)</param>
        /// <param name="queueName">Name of the Microsoft Message Queue.</param>
        /// <param name="applicationName">Application name</param>
        public MqTarget(string applicationName, string module, string queueName)
        {
            if (applicationName == null) throw new ArgumentNullException("applicationName");
            if (queueName == null) throw new ArgumentNullException("queueName");
            if (!MessageQueue.Exists(queueName))
                throw new ArgumentOutOfRangeException("queueName", queueName, "Queue do not exist.");

            _applicationName = applicationName;
            _module = module;
            _queue = new MessageQueue(queueName);
            _queue.Formatter = new XmlMessageFormatter();
        }

        #region ILogTarget Members

        /// <summary>
        /// Gets name of target. 
        /// </summary>
        /// <remarks>
        /// It must be unique for each target. The filename works for file targets etc.
        /// </remarks>
        public string Name
        {
            get { return "MQ: " + _queue.QueueName; }
        }

        /// <summary>
        /// Add a filter for this target.
        /// </summary>
        /// <param name="filter">Filters are used to validate if an entry can be written to a target or not.</param>
        public void AddFilter(IPostFilter filter)
        {
            if (filter == null) throw new ArgumentNullException("filter");
            _filters.AddLast(filter);
        }

        /// <summary>
        /// Enqueue to be written
        /// </summary>
        /// <param name="entry"></param>
        /// <remarks>
        /// The entry might be written directly in the same thread or enqueued to be written
        /// later. It's up to each implementation to decide. Keep in mind that a logger should not
        /// introduce delays in the thread execution. If it's possible that it will delay the thread,
        /// enqueue entries instead and write them in a seperate thread.
        /// </remarks>
        public void Enqueue(LogEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            if (_filters.Any(filter => !filter.CanLog(entry)))
            {
                return;
            }

            var dto = new LogEntryDTO(_applicationName, _module, entry);
            var message = new Message(dto);
            _queue.Send(message);
        }

        #endregion
    }
}