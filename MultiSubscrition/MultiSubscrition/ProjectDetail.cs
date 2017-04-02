using System;
using System.Runtime.Serialization;

namespace MultiSubscrition
{
    [DataContract]
    public class ProjectDetail
    {

        [DataMember]
        public int ProjectID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime ReleaseDate{ get; set; }
    }
}