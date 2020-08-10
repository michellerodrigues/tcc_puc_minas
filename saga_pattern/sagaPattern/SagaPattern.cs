using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Agropop.Saga.Factory;
using Agropop.Saga.Interfaces;
using Agropop.Saga.Messages;

namespace Agropop.Saga
{
    public class SagaPattern : MessageHandlerContext, IAmStartedByMessages<IMessage>, IHandleMessages<IMessage>
    {
      
        public SagaPattern(IChannel channel):base(channel){

        }
       
        public void StartSaga()
        {
            base.Consume();
        }

        public override Task Handle(BaseMessage message)
        {
            return base.Handle(message);
        }

        public Task Handle(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}