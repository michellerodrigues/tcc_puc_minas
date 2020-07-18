using System;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public interface IMessageBroker
    {
        IConnectionFactory Factory {get;}
        IConnection Connection {get;}
        IModel Model {get;}
    }
}