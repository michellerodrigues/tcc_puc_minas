using System;
using System.Threading.Tasks;
using Agropop.Saga;
using Agropop.Saga.Interfaces;
using Messages.Descartes.Commands;
using Messages.Descartes.Events;

namespace DescarteService.Services
{
    public class DescarteSaga : MessageHandlerContext, 
    IAmStartedByMessages<DescartePendenteCommand>, 
    IHandleMessages<AgendarRetiradaCommand>,
    IHandleMessages<DescartePendenteNotificadoEvent>
    {
        public DescarteSaga(IChannel channel) : base(channel)
        {
          //  base.Consume();
        }

        public Task Handle(AgendarRetiradaCommand message)
        {
            var evento = new AgendamentoRetiradaConfirmadoEvent()
            {
                Id = message.Id
            };

            base.Publish(evento);

            return Task.Delay(0);
        }

        public Task Handle (DescartePendenteNotificadoEvent evento)
        {
            var command = new AgendarRetiradaCommand()
            {
                Id = evento.Id
            };
            base.Publish(command);
            return Task.Delay(0);
        }

        public Task Handle(DescartePendenteCommand message)
        {
            var evento = new DescartePendenteNotificadoEvent()
            {
                Id = message.Id
            };
            base.Publish(evento);
            return Task.Delay(0);
        }
        public override T Cast<T>(object entity)
        {
            return base.Cast<T>(entity);
        } 
    }
}
