using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class ConcluirTriagemCommand : ICommand
    {
        public Guid Id { get; set; }
        public string EmailOperador { get; set; }
        public string SetorRetirada { get; set; }

        public ConcluirTriagemCommand()
        {
            
        }
        public ConcluirTriagemCommand(Guid id, string emailOperador, string setorRetirada)
        {
            this.Id = id;
            this.EmailOperador = emailOperador;
            this.SetorRetirada = setorRetirada;
        }
    }
}