using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class CancelarAtualizacaoEstoqueDescartadoCommand : ICommand
    {
        public Guid Id { get; set; }
        public string EmailSolicitante { get; set; }
        public DateTime DataSolicitacao { get; set; }

        public CancelarAtualizacaoEstoqueDescartadoCommand()
        {
            
        }
        public CancelarAtualizacaoEstoqueDescartadoCommand(Guid id, DateTime dataSolicitacao)
        {
            this.Id = id;
            this.DataSolicitacao = dataSolicitacao;
        }
    }
}