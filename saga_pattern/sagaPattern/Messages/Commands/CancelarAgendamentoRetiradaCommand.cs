using System;

namespace Agropop.Saga.Messages.Commands
{
    public class CancelarAgendamentoRetiradaCommand{
        public Guid Id { get; set; }
        public string EmailSolicitante { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public CancelarAgendamentoRetiradaCommand()
        {
            
        }
        public CancelarAgendamentoRetiradaCommand(Guid id, string emailSolicitante, DateTime dataSolicitacao)
        {
            this.Id = id;
            this.EmailSolicitante = emailSolicitante;
            this.DataSolicitacao = dataSolicitacao;
        }
    }
}