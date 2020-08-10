using Agropop.Saga.Messages;
using RabbitMQ.Client;

namespace Agropop.Saga.Interfaces
{
    public interface ISagaPattern : IAmStartedByMessages<IMessage>, IHandleMessages<IMessage>
    {

    }
}