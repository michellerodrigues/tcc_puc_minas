using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class AgendarRetiradaCommand : ICommand
    {
        public Guid Id { get; set; }
        public string EmailAgente { get; set; }
        public string LoteRetirada { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataRegistro { get; set; }
        public AgendarRetiradaCommand()
        {
            
        }
        public AgendarRetiradaCommand(Guid id, string emailAgente, string loteRetirada, DateTime dataAgendamento, DateTime dataRegistro)
        {
            this.Id = id;
            this.EmailAgente = emailAgente;
            this.LoteRetirada = loteRetirada;
            this.DataAgendamento = dataAgendamento;
            this.DataRegistro = dataRegistro;
        }
    }
}