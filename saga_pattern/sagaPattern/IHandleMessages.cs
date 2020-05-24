using System.Threading.Tasks;

namespace Agropop.Saga
{
    public interface IHandleMessages<T>
    {
        Task Handle(T message, IMessageHandlerContext context);
    }
}