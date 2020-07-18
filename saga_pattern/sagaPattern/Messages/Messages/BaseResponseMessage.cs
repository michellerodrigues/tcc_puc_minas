using System.Runtime.Serialization;

namespace Agropop.Descarte.Messages
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
