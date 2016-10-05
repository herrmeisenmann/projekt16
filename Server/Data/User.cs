using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    [DataContract]
    public class User
    {
        [DataMember]
        private int id;
        [DataMember]
        private string firstname;
        [DataMember]
        private string lastname;
        [DataMember]
        private string password;
        [DataMember]
        private Profession profession;
        [DataMember]
        private Classroom classroom;

        
        public User(int id, string firstname, string lastname, string password, Profession profession, Classroom classroom)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.password = password;
            this.profession = profession;
            this.classroom = classroom;
        }

        public User(int id, string firstname, string lastname, string password)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.password = password;
            this.profession = new Profession("NA");
            this.classroom = new Classroom("NA", "Descr. NA");
        }

    }
}
