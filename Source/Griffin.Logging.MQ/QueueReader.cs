using System;
using System.Messaging;
using Griffin.Logging.Net;

namespace Griffin.Logging.MQ
{
    /// <summary>
    /// Helper class which you can use to receive the logs from the queue
    /// </summary>
    public class QueueReader : IDisposable
    {
        private readonly MessageQueue _queue;
        private readonly string _queueName;
        private readonly ILogReceiver _receiver;
        private bool _shuttingDown;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueReader"/> class.
        /// </summary>
        /// <param name="queueName">Name of the message queue.</param>
        /// <param name="receiver">The implementation of this interface will be invoked for each received log entry.</param>
        public QueueReader(string queueName, ILogReceiver receiver)
        {
            if (queueName == null) throw new ArgumentNullException("queueName");
            if (receiver == null) throw new ArgumentNullException("receiver");
            _queueName = queueName;
            _receiver = receiver;

            if (!MessageQueue.Exists(queueName))
                throw new ArgumentOutOfRangeException("queueName", queueName, "Queue do not exist.");

            _queue = new MessageQueue(_queueName);
            _queue.ReceiveCompleted += OnReceived;
            _queue.Formatter = new XmlMessageFormatter(new[] {typeof (LogEntryDTO)});
            _queue.BeginReceive();
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _shuttingDown = true;
            _queue.Dispose();
        }

        #endregion

        private void OnReceived(object sender, ReceiveCompletedEventArgs e)
        {
            if (_shuttingDown)
                return;

            var msg = e.Message;
            _receiver.ReceivedLogEntry((LogEntryDTO) msg.Body);

            _queue.BeginReceive();
        }
    }
}