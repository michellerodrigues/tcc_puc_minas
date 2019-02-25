using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class CancelarConclusaoTriagemCommand : ICommand
    {
        public Guid Id { get; set; }

        public DateTime DataSolicitacao{ get; set; }

        public string EmailSolicitante{ get; set; }

        public CancelarConclusaoTriagemCommand()
        {
            
        }
        public CancelarConclusaoTriagemCommand(Guid id, string emailSolicitante, DateTime dataSolicitacao)
        {
            this.Id = id;
            this.DataSolicitacao = dataSolicitacao;
            this.EmailSolicitante = emailSolicitante;
        }
    }
}