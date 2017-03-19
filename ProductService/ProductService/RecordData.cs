using System.Runtime.Serialization;

namespace PrepareService
{
    [DataContract]
    public class RecordData
    {
        [DataMember]
        public int DataId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string MiddleName { get; set; }

        [DataMember]
        public string LastName { get; set; }
    }
}