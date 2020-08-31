using System;
using Messages.Descartes.Messages;

namespace Messages.Descartes.Events
{
    public class DescartePendenteNotificadoEvent : IEvent
    {
        public Guid Id {get;set;}
    }
}