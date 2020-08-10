using System;
using Agropop.Saga.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public class RabbitMQClient : IMessageBroker
    {
        private readonly IConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _model;


        public RabbitMQClient(IOptions<SagaConfigOptions> sagaConfig)
        {
            IConnectionFactory conn = new ConnectionFactory()
            {
                Uri = new Uri(sagaConfig.Value.Connection.RabbitMQSUrl)
            };

            _factory = conn;
            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();
        }

        public IConnectionFactory Factory => _factory;

        public IConnection Connection => _connection;

        public IModel Model => _model;
    }
}