using System.Threading.Tasks;

namespace Agropop.Saga.Interfaces
{
    public interface IHandleMessages<T>
    {        
        Task Handle(T message);
    }
}