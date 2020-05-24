using RabbitMQ.Client;

namespace Agropop.Saga
{
    public interface IConsumeOptions
    {
        IModel Channel {get;}
        IConnectionFactory Factory  {get;}
        IConnection Connection  {get;}
        IQueueOptions QueueOptions{get;}
    }
}