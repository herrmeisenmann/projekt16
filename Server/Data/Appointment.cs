using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    [DataContract]
    public class Appointment
    {
        [DataMember]
        public int id;
        [DataMember]
        public int userId;
        [DataMember]
        public string name;
        [DataMember]
        public string comment;
        [DataMember]
        public DateTime date;
        [DataMember]
        public string subject;
        [DataMember]
        public int grade;

        public Appointment(int id, int userId, string name, string comment, DateTime date, string subject, int grade)
        {
            this.id = id;
            this.userId = userId;
            this.name = name;
            this.comment = comment;
            this.date = date;
            this.subject = subject;
            this.grade = grade;
        }

    }
}
