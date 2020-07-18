
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agropop.Saga
{
    public interface IMessageHandlerContext
    {
        void  Publish(object message);
        List<string> Consume();
        //Task StartSaga();
    }
}