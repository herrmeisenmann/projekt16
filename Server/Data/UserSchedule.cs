using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    [DataContract]
    public class UserSchedule
    {
        [DataMember]
        private string appointment;
        [DataMember]
        private string description;

        public UserSchedule(string appointment, string description)
        {
            this.appointment = appointment;
            this.description = description;
        }

    }
}
