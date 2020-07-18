using System.Threading.Tasks;

namespace Agropop.Saga
{
    public interface ISagaStartMessage <T>: IHandleMessages<T>
    {        
    }
}