using System;
using System.Runtime.Serialization;

namespace MultiSubscrition
{
    [DataContract]
    public class TaskDetail
    {
        [DataMember]
        public int TaskId { get; set; }

        [DataMember]
        public int ProjectID { get; set; }

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public Priority Priority { get; set; }

        [DataMember]
        public DateTime DueDate { get; set; }

        [DataMember]
        public DateTime CompletedDate { get; set; }

        [DataMember]
        public bool IsComplete { get; set; }
    }
}