using System;

namespace Messages.Descartes.Commands
{
    public class TriagemRealizadaEvent
    {
        public Guid Id { get; set; }
        public DateTime DataRealizacao { get; set; }
        public string EmailOperador{ get; set; }

        public TriagemRealizadaEvent()
        {
            
        }
        public TriagemRealizadaEvent(Guid id, string emailOperador, string loteTriagem, DateTime dataRealizacao)
        {
            this.Id = id;
            this.EmailOperador = emailOperador;
            this.DataRealizacao = dataRealizacao;
        }
    }
}