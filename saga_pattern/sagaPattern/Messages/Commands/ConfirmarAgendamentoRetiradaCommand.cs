using System;


namespace Agropop.Saga.Messages.Commands
{
    public class ConfirmarAgendamentoRetiradaCommand
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