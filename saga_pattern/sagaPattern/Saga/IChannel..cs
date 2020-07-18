using RabbitMQ.Client;

namespace Agropop.Saga
{
    public interface IChannel
    {
        IMessageBroker MessageBroker {get;}
        IQueueOptions QueueOptions {get;}
        IExchangeOptions ExchangeOptions {get;}
    }
}