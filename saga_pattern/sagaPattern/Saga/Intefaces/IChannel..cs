using RabbitMQ.Client;

namespace Agropop.Saga.Interfaces
{   
    public interface IChannel
    {
        IMessageBroker MessageBroker {get;}
        IQueueOptions QueueOptions {get;}
        IExchangeOptions ExchangeOptions {get;}
    }
}