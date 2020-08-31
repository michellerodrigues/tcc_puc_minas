using System.Threading.Tasks;

namespace Agropop.Saga.Interfaces
{
    public interface IHandleMessages<ICommand>
    {        
        Task Handle(ICommand message);
    }
}