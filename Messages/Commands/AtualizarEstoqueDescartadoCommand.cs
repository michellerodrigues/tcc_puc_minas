using System;
using NServiceBus;

namespace Messages.Descartes.Commands
{
    public class AtualizarEstoqueDescartadoCommand : ICommand
    {
        public Guid Id { get; set; }
        public DateTime DataDescarte { get; set; }

        public AtualizarEstoqueDescartadoCommand()
        {
            
        }
        public AtualizarEstoqueDescartadoCommand(Guid id, DateTime dataDescarte)
        {
            this.Id = id;
            this.DataDescarte = dataDescarte;
        }
    }
}