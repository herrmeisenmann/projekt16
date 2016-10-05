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
        public int id;
        [DataMember]
        public string name;
        [DataMember]
        public int timetable_id;

        public Classroom(int id, string name, int timetable_id)
        {
            this.id = id;
            this.name = name;
            this.timetable_id = timetable_id;
        }
    }
}
