using System;


namespace Messages.Descartes.Commands
{
    public class RealizarRetiradaDescarteCommand 
    {
        public Guid Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public RealizarRetiradaDescarteCommand()
        {
            
        }
        public RealizarRetiradaDescarteCommand(Guid id, DateTime dataSolicitacao)
        {
            this.Id = id;
            this.DataSolicitacao = dataSolicitacao;
        }
    }
}