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
    /// <summary>
    /// Interface für den WCF-Service
    /// Alle hier aufgeführten Methoden stehen dem Konsumenten(Client) zur verfügung
    /// </summary>
    [ServiceContract]
    interface IService
    {
        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        User GetUserByName(string name);


        [OperationContract]
        bool InsertUserIntoDb(string username, string firstname, string lastname, string password, int profession_id, int class_id);

        [OperationContract]
        bool LoginUser(string name, string password);

        [OperationContract]
        List<Appointment> GetAppointmentsByUserId(int id);

        [OperationContract]
        bool InsertUserAppointment(int userId, string name, string comment, DateTime date, string subject, int grade);

        [OperationContract]
        Classroom GetClassByUserId(int userId);

        [OperationContract]
        Stream GetClassScheduleById(int id);

        [OperationContract]
        List<Classroom> GetAllClasses();

        [OperationContract]
        List<Profession> GetAllProfessions();

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        List<String> GetChatMessages(int amount);

        [OperationContract]
        void WriteToChat(string user, string message);
    }
}
