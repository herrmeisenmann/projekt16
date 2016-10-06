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
        public int id;
        [DataMember]
        public string username;
        [DataMember]
        public string firstname;
        [DataMember]
        public string lastname;
        [DataMember]
        public string password;
        [DataMember]
        public Profession profession;
        [DataMember]
        public Classroom classroom;

        
        public User(int id, string username, string firstname, string lastname, string password, Profession profession, Classroom classroom)
        {
            this.id = id;
            this.username = username;
            this.firstname = firstname;
            this.lastname = lastname;
            this.password = password;
            this.profession = profession;
            this.classroom = classroom;
        }
    }
}
