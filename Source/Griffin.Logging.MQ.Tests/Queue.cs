using System.Messaging;

namespace Griffin.Logging.MQ.Tests
{
    class Queue
    {
        public const string Name = @".\private$\Logging";

        public static void Reset()
        {
            if (!MessageQueue.Exists(Queue.Name))
                MessageQueue.Create(Queue.Name);
            else
            {
                var queue = new MessageQueue(Queue.Name);
                queue.GetAllMessages();
            }
        }
    }
}
