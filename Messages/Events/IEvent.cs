using System;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Events
{
    public interface IEvent:IMessage
    {
        Guid Id { get; }    
    }
}
