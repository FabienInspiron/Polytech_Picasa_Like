using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ObjetsDefinition
{
    [DataContractAttribute]
    public class Role
    {
        [DataMemberAttribute]
        public int Id { get; set; }

        [DataMemberAttribute]
        public String User { get; set; }

        [DataMemberAttribute]
        public String LeRole { get; set; }

        public Role(String us, String ro) : this(-1, us, ro)
        { }

        public Role(int id, String us, String ro)
        {
            Id = id;
            User = us;
            LeRole = ro;
        }
    }
}
