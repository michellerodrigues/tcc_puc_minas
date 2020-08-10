using System.Collections.Generic;
using Agropop.Saga.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Agropop.Saga
{
    public class ExchangeOptions : IExchangeOptions
    {
        private readonly  string _exchange;
        private readonly  string _type;
        private readonly  string _routingKey;
        private readonly  bool _durable;
        private readonly  string _alternateExchange;
        private readonly SagaConfigOptions _sagaConfigValue;

        public ExchangeOptions(IMessageBroker messageBroker, IOptions<SagaConfigOptions> sagaConfig)
        {   
            _sagaConfigValue =sagaConfig.Value;
            _exchange = _sagaConfigValue.ExchangeOptions.Name;
            _type = _sagaConfigValue.ExchangeOptions.Type;
            _routingKey = _sagaConfigValue.ExchangeOptions.RoutingKey;
            _durable = _sagaConfigValue.ExchangeOptions.Durable;
            _alternateExchange = _sagaConfigValue.ExchangeOptions.AlternateExchange;
            
            bool autodelete=false;

            IDictionary<string, object> arguments = new Dictionary<string, object>();
            arguments.Add("alternate-exchange",_alternateExchange);
            messageBroker.Model.ExchangeDeclare(exchange: _exchange, type: _type, durable: _durable, autoDelete:autodelete, arguments:arguments);            
        }

        public string Exchange => _exchange;
        public string Type => _type;
        public string RoutingKey => _routingKey;
        public bool Durable => _durable;
        public string AlternateExchange => _alternateExchange;
    }
}