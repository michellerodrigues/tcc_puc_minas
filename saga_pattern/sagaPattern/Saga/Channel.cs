using System.Collections.Generic;
using Agropop.Saga.Interfaces;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class Channel : IChannel
    {        
        private readonly IMessageBroker _messageBroker;
        private readonly IQueueOptions _queueOptions;
        private readonly IExchangeOptions _exchangeOptions;
        
        public Channel(IMessageBroker messageBroker, IQueueOptions queueOptions, IExchangeOptions exchangeOptions)
        {
            _queueOptions = queueOptions;
            _exchangeOptions = exchangeOptions;
            
           /* messageBroker.Model.QueueDeclare(
                queue: queueOptions.Queue,
                durable: queueOptions.Durable,
                exclusive: queueOptions.Exclusive,
                autoDelete: queueOptions.AutoDelete,
                arguments: queueOptions.Arguments);    */

            //_exchangeOptions = exchangeOptions;
          //  IDictionary<string, object> arguments = new Dictionary<string, object>();
     //   arguments.Add("alternate-exchange",exchangeOptions.AlternateExchange);
        //    messageBroker.Model.ExchangeDeclare(exchange: exchangeOptions.Exchange, type: exchangeOptions.Type, durable: exchangeOptions.Durable, arguments:arguments);
            _messageBroker = messageBroker;
        }

        public IQueueOptions QueueOptions => _queueOptions;

        public IExchangeOptions ExchangeOptions => _exchangeOptions;

        public IMessageBroker MessageBroker => _messageBroker;
    }
}