using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Server.Data;
using System.Drawing;
using System.IO;

namespace Server
{
    public class Service : IService
    {
        private DbConnector dbConnector = new DbConnector();

        public List<Classroom> GetAllClasses()
        {
            return dbConnector.GetAllClasses();
        }

        public Classroom GetClass(User user)
        {
            Classroom classroom = new Classroom("DummyKlasse", "Dies ist eine dummyklasse");

            return classroom;
        }

        public Stream GetClassScheduleById(int id)
        {
            string filepath = $"C:\\projects\\projekt16\\Server\\stundenplan\\{id}.png";
            if(File.Exists(filepath))
            {
                return new FileStream(filepath, FileMode.Open, FileAccess.Read);
            }
            else
            {
                return null;
            }
        }

        public User GetUserById(int id)
        {
            return dbConnector.GetUserById(id);
        }

        public UserSchedule GetUserScheduleByUserId(int id)
        {
            return new UserSchedule("Termin", "Terminbeschreibung");
        }

        public bool InsertNewUserAppointment(int id, UserSchedule appointment)
        {

            return dbConnector.InsertUserAppointment(id, appointment);
        }

        public bool InsertUserIntoDb(User user)
        {
            if (Utility.ValidateUser(user))
            {
                return dbConnector.InsertUser(user);
            }
            else
            {
                return false;
            }
        }

        public bool LoginUser(String name, String password)
        {
            return true;
        }
    }
}
