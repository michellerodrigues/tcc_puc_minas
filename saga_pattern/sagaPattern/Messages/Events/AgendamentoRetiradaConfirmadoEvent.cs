using System;


namespace Agropop.Saga.Messages.Events
{
    public class AgendamentoRetiradaConfirmadoEvent : IEvent
{
    public Guid Id { get; set; }
    public DateTime ConfirmadoEm { get; set; }

    public string EmailConfirmacao { get; set; }

    public AgendamentoRetiradaConfirmadoEvent()
    {

    }
    public AgendamentoRetiradaConfirmadoEvent(Guid id, string emailConfirmacao)
    {
        Id = id;
        ConfirmadoEm = DateTime.Now;
        EmailConfirmacao = emailConfirmacao;
    }
}
}
