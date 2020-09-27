using System.Collections.Generic;
using Agropop.Saga.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Agropop.Saga
{
    public class QueueOptions : IQueueOptions
    {
        private readonly string _queue;
        private readonly  bool _durable;
        private readonly  bool _exclusive;
        private readonly  bool _autoDelete;
        private readonly  IDictionary<string, object>  _arguments;
        private readonly SagaConfigOptions _sagaConfigValue;


        public QueueOptions(IMessageBroker messageBroker,  IOptions<SagaConfigOptions> sagaConfig)
        {   
            _sagaConfigValue = sagaConfig.Value;

            _queue = _sagaConfigValue.Queue.Name;
            _durable = _sagaConfigValue.Queue.Durable;
            _exclusive = _sagaConfigValue.Queue.Exclusive;
            _autoDelete = _sagaConfigValue.Queue.AutoDelete;
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