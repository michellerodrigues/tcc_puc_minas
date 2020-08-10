using RabbitMQ.Client;

namespace Agropop.Saga.Interfaces
{
    public interface IConsumeOptions
    {
        IModel Channel {get;}
        IQueueOptions QueueOptions{get;}
    }
}