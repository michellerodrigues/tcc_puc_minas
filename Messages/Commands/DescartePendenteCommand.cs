using System;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Commands
{
    public class DescartePendenteCommand:ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string LoteDeDescarte { get; set; }
    }
}