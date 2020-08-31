using System;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Commands
{
    public class AgendarRetiradaCommand:ICommand
    {
        public Guid Id { get; set; }
        public string EmailAgente { get; set; }
        public string LoteRetirada { get; set; }
        public string DataAgendamento { get; set; }
        public DateTime DataRegistro { get; set; }
        public string Status { get; set; }="ok";
        public string Tipo { get; set; }
        public AgendarRetiradaCommand()
        {
            
        }
        public AgendarRetiradaCommand(Guid id, string emailAgente, string loteRetirada, string dataAgendamento, DateTime dataRegistro, string status)
        {
            this.Id = id;
            this.EmailAgente = emailAgente;
            this.LoteRetirada = loteRetirada;
            this.DataAgendamento = dataAgendamento;
            this.DataRegistro = dataRegistro;
            this.Status =  status;
        }
    }
}