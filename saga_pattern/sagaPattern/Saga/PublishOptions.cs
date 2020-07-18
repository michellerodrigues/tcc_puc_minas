using System.Collections.Generic;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class PublishOptions : IPublishOptions
    {
        private readonly IModel _channel;
        private readonly IExchangeOptions _exchangeOptions;
        public PublishOptions(IModel channel, IExchangeOptions exchangeOptions)
        {
            _exchangeOptions = exchangeOptions;
            IDictionary<string, object> arguments = new Dictionary<string, object>();
            arguments.Add("alternate-exchange",_exchangeOptions.AlternateExchange);
            _channel.ExchangeDeclare(exchange: _exchangeOptions.Exchange, type: _exchangeOptions.Type, durable: _exchangeOptions.Durable, arguments:arguments);
        }
        public IModel Channel => _channel;
        public IExchangeOptions ExchangeOptions => _exchangeOptions;

     //   public IConnectionFactory Factory => throw new System.NotImplementedException();

 //       public IConnection Connection => throw new System.NotImplementedException();
    }
}