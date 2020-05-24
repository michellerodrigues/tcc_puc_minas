using System.Collections.Generic;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class QueueOptions : IQueueOptions
    {
        public readonly string _queue;
        public readonly bool _durable;
        public readonly bool _exclusive;
        public readonly bool _autoDelete;
        public readonly IDictionary<string, object> _arguments;
        public QueueOptions(string queue, bool durable, bool exclusive, bool autoDelete, IDictionary<string, object> arguments)
        {
            _queue = queue;
            _durable = durable;
            _exclusive = exclusive;
            _autoDelete = autoDelete;
            _arguments = arguments;

        }
        public string Queue => _queue;
        public bool Durable => _durable;
        public bool Exclusive => _exclusive;
        public bool AutoDelete => _autoDelete;
        public IDictionary<string, object> Arguments => _arguments;       
    }
}