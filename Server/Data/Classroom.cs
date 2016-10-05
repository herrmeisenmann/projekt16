using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    [DataContract]
    public class Classroom
    {
        [DataMember]
        private string name;
        [DataMember]
        private string description;

        public Classroom(string name, string description)
        {
            this.name = name;
            this.description = description;
        }
    }
}
