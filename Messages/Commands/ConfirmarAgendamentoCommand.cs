using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class ConfirmarAgendamentoCommand : ICommand
    {
        public Guid Id { get; set; }
        public DateTime SolicitadoEm { get; set; }
        public string EmailSolicitacao { get; set; }
        public ConfirmarAgendamentoCommand()
        {
            
        }
        public ConfirmarAgendamentoCommand(Guid id, DateTime solicitadoEm, string emailSolicitacao)
        {
            this.Id = id;
            this.SolicitadoEm = solicitadoEm;
            this.EmailSolicitacao = emailSolicitacao;
        }
    }
}