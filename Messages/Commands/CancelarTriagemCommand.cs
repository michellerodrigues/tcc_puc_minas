using System;


namespace Messages.Descartes.Commands
{
    public class CancelarTriagemCommand
    {
        public Guid Id { get; set; }
        public string EmailSolicitante { get; set; }
        public DateTime DataSolicitacao { get; set; }

        public CancelarTriagemCommand()
        {
            
        }
        public CancelarTriagemCommand(Guid id, string emailSolicitante, DateTime dataSolicitacao)
        {
            this.Id = id;
            this.EmailSolicitante = emailSolicitante;
            this.DataSolicitacao = dataSolicitacao;
        }
    }
}