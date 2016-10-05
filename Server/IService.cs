using Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;
using System.IO;

namespace Server
{
    [ServiceContract]
    interface IService
    {
        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        bool InsertUserIntoDb(User user);

        [OperationContract]
        bool LoginUser(string name, string password);

        [OperationContract]
        UserSchedule GetUserScheduleByUserId(int id);

        [OperationContract]
        bool InsertNewUserAppointment(int id, UserSchedule appointment);

        [OperationContract]
        Classroom GetClass(User user);

        [OperationContract]
        Stream GetClassScheduleById(int id);

        [OperationContract]
        List<Classroom> GetAllClasses();
    }
}
