using System.Runtime.Serialization;

namespace Messages.Descartes.Messages
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
