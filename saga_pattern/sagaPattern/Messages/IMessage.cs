using System;

namespace Agropop.Saga.Messages
{
    public interface IMessage
    {
        Guid Id { get; }    
    }
}
