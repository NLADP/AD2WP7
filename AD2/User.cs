using System;
using System.Runtime.Serialization;

namespace AD2
{
    [DataContract]
    public class User
    {
        [DataMember]
        public Guid Id { get; internal set; }

        [DataMember]
        public string Name { get; internal set; }
    }
}
