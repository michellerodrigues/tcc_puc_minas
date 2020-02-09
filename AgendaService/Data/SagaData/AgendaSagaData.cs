using System;
using NServiceBus;

namespace AgendaService.Data.SagaData
{
    public class AgendaSagaData : ContainSagaData
    {
        public virtual Guid AgendaId { get; set; }

        public virtual string Tipo { get; set; }
    }
}