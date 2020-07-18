using System;


namespace Agropop.Saga.Messages.Commands
{
    public class CancelarAgendamentoRetiradaConfirmadaCommand 
    {
       public Guid Id { get; set; }
        public string EmailSolicitante { get; set; }
        
        public CancelarAgendamentoRetiradaConfirmadaCommand()
        {
            
        }
        public CancelarAgendamentoRetiradaConfirmadaCommand(Guid id, string emailSolicitante)
        {
            this.Id = id;
            this.EmailSolicitante = emailSolicitante;
        }
    }
}