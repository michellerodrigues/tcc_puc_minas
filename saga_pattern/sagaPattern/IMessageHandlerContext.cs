
namespace Agropop.Saga
{
    public interface IMessageHandlerContext
    {   
        void Publish(object message, IPublishOptions options);
        void Consume(IConsumeOptions options);
    }
}