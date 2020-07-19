using RabbitMQ.Client;

namespace Agropop.Saga
{   public interface IPublishOptions
    {    
        IChannel Channel{get;}   
    }
}