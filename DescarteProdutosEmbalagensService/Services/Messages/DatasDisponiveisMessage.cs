using System.Runtime.Serialization;

namespace DescarteService.Services.Messages
{
    [DataContract]
    public class DatasDisponiveisMessage
    {
        [DataMember(Name = "data")]
        public DateTimeFormat Data { get; set; }

        [DataMember(Name = "linkAgendamento")]
        public string LinkAgendamento { get; set; }
    }
}
