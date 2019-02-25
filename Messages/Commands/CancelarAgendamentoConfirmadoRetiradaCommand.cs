using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class CancelarAgendamentoConfirmadoRetiradaCommand : ICommand
    {
       public Guid Id { get; set; }
        public string EmailSolicitante { get; set; }
        
        public CancelarAgendamentoConfirmadoRetiradaCommand()
        {
            
        }
        public CancelarAgendamentoConfirmadoRetiradaCommand(Guid id, string emailSolicitante)
        {
            this.Id = id;
            this.EmailSolicitante = emailSolicitante;
        }
    }
}