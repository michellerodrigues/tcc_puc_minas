
using System.Collections.Generic;
using System.Threading.Tasks;
using Messages.Descartes.Commands;

namespace Agropop.Saga.Interfaces
{
    public interface IMessageHandlerContext
    {
        void  Publish(object message);
        List<string> Consume();
    }
}