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
        private string name;

        public Profession(string name)
        {
            this.name = name;
        }
    }
}
