using System;

namespace Messages.Descartes.Events
{
    public class RetiradaAgendadaEvent
    {
        public Guid Id { get; set; }
        public DateTime DataRegistro { get; set; }
        public string EmailSolicitante { get; set; }
        public RetiradaAgendadaEvent()
        {
            
        }
        public RetiradaAgendadaEvent(Guid id, string emailSolicitante)
        {
            this.Id = id;
            this.DataRegistro = DateTime.Now;
            this.EmailSolicitante = emailSolicitante;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}