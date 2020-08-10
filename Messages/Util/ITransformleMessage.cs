using System.Threading.Tasks;
using Saga.Messages.Base;

namespace Agropop.Saga.Interfaces
{
    public interface ITransformleMessage<T>
    {        
        BaseMessage TransformTOBaseMessage(T message);
    }
}