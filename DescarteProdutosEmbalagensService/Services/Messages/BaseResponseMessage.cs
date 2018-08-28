using System.Runtime.Serialization;

namespace DescarteService.Services.Messages
{
    [DataContract]
    public class BaseResponseMessage
    {
        [DataMember(Name = "codRetorno")]
        public int codRetorno { get; set; }

        [DataMember(Name = "statusRetorno")]
        public string StatusRetorno { get; set; }
    }
}
