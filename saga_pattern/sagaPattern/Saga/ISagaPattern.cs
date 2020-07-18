using Agropop.Saga.Messages;
using RabbitMQ.Client;

namespace Agropop.Saga
{
    public interface ISagaPattern : IAmStartedByMessages<IMessage>, IHandleMessages<IMessage>
    {

    }
}