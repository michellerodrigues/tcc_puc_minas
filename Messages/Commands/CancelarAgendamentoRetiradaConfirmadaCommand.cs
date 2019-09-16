using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class CancelarAgendamentoRetiradaConfirmadaCommand : ICommand
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