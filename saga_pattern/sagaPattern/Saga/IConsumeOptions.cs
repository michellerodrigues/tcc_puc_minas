using RabbitMQ.Client;

namespace Agropop.Saga
{
    public interface IConsumeOptions
    {
        IModel Channel {get;}
        IQueueOptions QueueOptions{get;}
    }
}