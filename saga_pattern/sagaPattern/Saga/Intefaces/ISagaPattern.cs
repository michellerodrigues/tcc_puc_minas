using Agropop.Saga.Messages;
using Agropop.Saga.Messages.Commands;
using Agropop.Saga.Messages.Events;

namespace Agropop.Saga.Interfaces
{
    public interface ISagaPattern : IAmStartedByMessages<IMessage>, IHandleMessages<ICommand>
    {

    }
}