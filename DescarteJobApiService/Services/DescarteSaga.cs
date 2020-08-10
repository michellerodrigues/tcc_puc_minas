using System.Threading.Tasks;
using Agropop.Saga;
using Agropop.Saga.Interfaces;
using Messages.Descartes.Messages;


namespace DescarteService.Services
{
    public class DescarteSaga : MessageHandlerContext, IAmStartedByMessages<IMessage>, IHandleMessages<IMessage>
    {
        public DescarteSaga(IChannel channel) : base(channel)
        {
            base.Consume();
        }

        public Task Handle(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}
