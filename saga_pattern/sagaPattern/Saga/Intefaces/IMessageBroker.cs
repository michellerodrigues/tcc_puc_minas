using System;
using RabbitMQ.Client;

namespace Agropop.Saga.Interfaces
{
    public interface IMessageBroker
    {
        IConnectionFactory Factory {get;}
        IConnection Connection {get;}
        IModel Model {get;}
    }
}