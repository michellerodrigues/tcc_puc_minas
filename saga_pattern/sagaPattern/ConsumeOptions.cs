using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class ConsumeOptions : IConsumeOptions
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IQueueOptions _queueOptions;
        public ConsumeOptions(IConnectionFactory factory, IQueueOptions queueOptions)
        {
            _factory = factory;
            _queueOptions = queueOptions;
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: queueOptions.Queue,
                durable: queueOptions.Durable,
                exclusive: queueOptions.Exclusive,
                autoDelete: queueOptions.AutoDelete,
                arguments: queueOptions.Arguments);
        }
        public IModel Channel => _channel;
        public IQueueOptions QueueOptions => _queueOptions;
        public IConnectionFactory Factory => _factory;
        public IConnection Connection => _connection;
    }
}