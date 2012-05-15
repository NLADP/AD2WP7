using System;
using System.Runtime.Serialization;

namespace AD2
{
    [DataContract]
    public class WorkItem
    {
        [DataMember]
        public Guid Id { get; internal set; }

        [DataMember]
        public string Name { get; internal set; }

        [DataMember]
        public Guid Status { get; internal set; }

        [DataMember]
        public int Complexity { get; internal set; }

        public override bool Equals(object obj)
        {
            return obj is WorkItem ? Equals((WorkItem)obj) : base.Equals(obj);
        }

        public bool Equals(WorkItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
