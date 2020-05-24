using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class PublishOptions : IPublishOptions
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IExchangeOptions _exchangeOptions;
        public PublishOptions(IConnectionFactory factory, IExchangeOptions exchangeOptions)
        {
            _factory = factory;
            _exchangeOptions = exchangeOptions;
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: exchangeOptions.Exchange, type: exchangeOptions.Type);
        }
        public IModel Channel => _channel;
        public IExchangeOptions ExchangeOptions => _exchangeOptions;
        public IConnectionFactory Factory => _factory;
        public IConnection Connection => _connection;
    }
}