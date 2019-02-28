using System;
using NServiceBus;

namespace Messages.Descartes.Events
{
    public class AgendamentoConfirmadoEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime ConfirmadoEm { get; set; }

        public string EmailConfirmacao { get; set; }

        public AgendamentoConfirmadoEvent()
        {
            
        }
        public AgendamentoConfirmadoEvent(Guid id, string emailConfirmacao)
        {
            this.Id = id;
            this.ConfirmadoEm = DateTime.Now;
            this.EmailConfirmacao = emailConfirmacao;
        }
    }
}
