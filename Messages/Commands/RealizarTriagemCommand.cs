using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class RealizarTriagemCommand : ICommand
    {
        public Guid Id { get; set; }
        public string EmailAgente { get; set; }
        public string LoteTriagem { get; set; }
        public DateTime DataEntrada { get; set; }
        public string SetorRetirada { get; set; }


        public RealizarTriagemCommand()
        {
            
        }
        public RealizarTriagemCommand(Guid id, string emailAgente, string loteTriagem, DateTime dataEntrada, string setorRetirada)
        {
            this.Id = id;
            this.EmailAgente = emailAgente;
            this.DataEntrada = dataEntrada;
            this.LoteTriagem = loteTriagem;
            this.SetorRetirada = setorRetirada;
        }
    }
}