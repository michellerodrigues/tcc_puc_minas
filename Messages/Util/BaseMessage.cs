
namespace Saga.Messages.Base
{
    public class BaseMessage
    {
        public object content {get;set;}   
        public string fullNameType {get;set;}
        public string assemblyName {get;set;}
        public string handleMethod {get;set;}

        public string UserForNewType {get;set;}
    }
}
    