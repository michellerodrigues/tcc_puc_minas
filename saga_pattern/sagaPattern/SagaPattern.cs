using System;
using System.Threading.Tasks;
using Agropop.Saga.Interfaces;
using Agropop.Saga.Messages;
using Agropop.Saga.Messages.Commands;

namespace Agropop.Saga
{
    public class SagaPattern : MessageHandlerContext, IAmStartedByMessages<IMessage>, 
    IHandleMessages<ICommand>
    {
      
        public SagaPattern(IChannel channel):base(channel){

        }
       
        public void StartSaga()
        {
            base.Consume();
        }

        public Task Handle(IMessage message)
        {
            throw new NotImplementedException();
        }

        Task IHandleMessages<ICommand>.Handle(ICommand message)
        {
            throw new NotImplementedException();
        }
    }
}