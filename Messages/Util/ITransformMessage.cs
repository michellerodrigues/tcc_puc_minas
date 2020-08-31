
using Saga.Messages.Base;

namespace Agropop.Saga.Interfaces
{
    public interface ITransformMessage<IMessage>
    {        
        BaseMessage TransformTOBaseMessage(IMessage message);
    }
}