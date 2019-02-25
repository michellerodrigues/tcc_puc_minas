using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class RealizarDescarteCommand : ICommand
    {
        public Guid Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public RealizarDescarteCommand()
        {
            
        }
        public RealizarDescarteCommand(Guid id, DateTime dataSolicitacao)
        {
            this.Id = id;
            this.DataSolicitacao = dataSolicitacao;
        }
    }
}