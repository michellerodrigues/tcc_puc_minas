using RabbitMQ.Client;

namespace Agropop.Saga.Interfaces
{   public interface IPublishOptions
    {    
        IChannel Channel{get;}   
    }
}