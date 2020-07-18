using System.Collections.Generic;

namespace Agropop.Saga
{
    public class QueueOptions : IQueueOptions
    {
        private readonly string _queue;
        private readonly  bool _durable;
        private readonly  bool _exclusive;
        private readonly  bool _autoDelete;
        private readonly  IDictionary<string, object>  _arguments;

        public QueueOptions(IMessageBroker messageBroker,SagaConfig sagaConfig)
        {   
            _queue = sagaConfig.QueueOptions.Queue;
            _durable = sagaConfig.QueueOptions.Durable;
            _exclusive = sagaConfig.QueueOptions.Exclusive;
            _autoDelete = sagaConfig.QueueOptions.AutoDelete;
            _arguments = null;

            messageBroker.Model.QueueDeclare(
                queue: _queue,
                durable: _durable,
                exclusive: _exclusive,
                autoDelete: _autoDelete,
                arguments: _arguments);  
        }
        public string Queue => _queue;
        public bool Durable => _durable;
        public bool Exclusive => _exclusive;
        public bool AutoDelete => _autoDelete;
        public IDictionary<string, object> Arguments => _arguments;       
    }
}