using System.Collections.Generic;

namespace Agropop.Saga
{
    public class ExchangeOptions : IExchangeOptions
    {
        private readonly  string _exchange;
        private readonly  string _type;
        private readonly  string _routingKey;
        private readonly  bool _durable;
        private readonly  string _alternateExchange;

        public ExchangeOptions(IMessageBroker messageBroker, SagaConfig sagaConfig)
        {   
            _exchange = sagaConfig.ExchangeOptions.Exchange;
            _type = sagaConfig.ExchangeOptions.Type;
            _routingKey = sagaConfig.ExchangeOptions.RoutingKey;
            _durable = sagaConfig.ExchangeOptions.Durable;
            _alternateExchange = sagaConfig.ExchangeOptions.AlternateExchange;
            
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