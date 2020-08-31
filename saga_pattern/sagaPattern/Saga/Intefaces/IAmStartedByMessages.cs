using System.Threading.Tasks;

namespace Agropop.Saga.Interfaces
{
    public interface IAmStartedByMessages<IMessage>
    {
        Task Handle(IMessage message);
    }
}