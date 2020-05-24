using RabbitMQ.Client;

namespace Agropop.Saga
{   public interface IPublishOptions
    {      
        IConnectionFactory Factory {get;}
        IConnection Connection{get;}
        IModel Channel {get;}
        IExchangeOptions ExchangeOptions{get;}       
    }
}