using System;


namespace Agropop.Saga.Messages.Events
{
    public class TriagemCanceladaEvent:IEvent
    {
        public Guid Id { get; set; }

        public string EmailRealizador { get; set; }

        public DateTime DataCancelamento { get; set; }
        public TriagemCanceladaEvent()
        {
            
        }
        public TriagemCanceladaEvent(Guid id, string emailRealizador)
        {
            this.Id = id;
            this.EmailRealizador = emailRealizador;
            this.DataCancelamento = DateTime.Now;
        }
    }
}