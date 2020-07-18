using RabbitMQ.Client;

namespace Agropop.Saga
{   public interface IPublishOptions
    {    
        IExchangeOptions ExchangeOptions{get;}   
        IModel Channel {get;} 
   //     IConnectionFactory Factory {get;} 
   //     IConnection Connection {get;} 
    }
}