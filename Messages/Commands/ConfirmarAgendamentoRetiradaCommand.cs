using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class ConfirmarAgendamentoRetiradaCommand : ICommand
    {
        public Guid Id { get; set; }
        public DateTime SolicitadoEm { get; set; }
        public string EmailSolicitacao { get; set; }
        public ConfirmarAgendamentoRetiradaCommand()
        {
            
        }
        public ConfirmarAgendamentoRetiradaCommand(Guid id, DateTime solicitadoEm, string emailSolicitacao)
        {
            this.Id = id;
            this.SolicitadoEm = solicitadoEm;
            this.EmailSolicitacao = emailSolicitacao;
        }
    }
}