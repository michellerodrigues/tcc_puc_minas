using Agropop.Saga.Interfaces;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class ConsumeOptions : IConsumeOptions
    {
        private readonly IModel _channel;
        private readonly IQueueOptions _queueOptions;
        public ConsumeOptions(IModel channel, IQueueOptions queueOptions)
        {
            channel.QueueDeclare(
                queue: queueOptions.Queue,
                durable: queueOptions.Durable,
                exclusive: queueOptions.Exclusive,
                autoDelete: queueOptions.AutoDelete,
                arguments: queueOptions.Arguments); 

           _channel = channel;
           _queueOptions = queueOptions;               
        }

        public IModel Channel => _channel;
        public IQueueOptions QueueOptions => _queueOptions;
    }
}