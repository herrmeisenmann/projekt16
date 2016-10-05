using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    [DataContract]
    public class Profession
    {
        [DataMember]
        public int id;
        [DataMember]
        public string name;

        public Profession(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
