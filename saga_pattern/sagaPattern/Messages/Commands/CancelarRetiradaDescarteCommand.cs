using System;


namespace Agropop.Saga.Messages.Commands
{
    public class CancelarRetiradaDescarteCommand 
    {
        public Guid Id { get; set; }
        public string EmailSolicitante { get; set; }
        public DateTime DataSolicitacao { get; set; }

        public CancelarRetiradaDescarteCommand()
        {
            
        }
        public CancelarRetiradaDescarteCommand(Guid id, string emailSolicitante, DateTime dataSolicitacao)
        {
            this.Id = id;
            this.EmailSolicitante = emailSolicitante;
            this.DataSolicitacao = dataSolicitacao;
        }
    }
}