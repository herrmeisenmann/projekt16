﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Server.Data;
using System.Drawing;
using System.IO;
using Server;

namespace Server
{
    public class Service : IService
    {
        private DbConnector dbConnector = new DbConnector();
        private ChatHandler chatHandler = new ChatHandler();

        /// <summary>
        /// Gibt alle Klassen aus der Datenbank wieder
        /// </summary>
        /// <returns>List of all classes</returns>
        public List<Classroom> GetAllClasses()
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return dbConnector.GetAllClasses();
        }

        /// <summary>
        /// Gibt eine Liste aller User zurück
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllUsers()
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return dbConnector.GetAllUsers();
        }

        /// <summary>
        /// Gibt die Klasse eines Users anhand seiner Id wieder
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Classroom GetClassByUserId(int userId)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            Classroom classroom = dbConnector.GetUserById(userId).classroom;

            return classroom;
        }

        /// <summary>
        /// Gibt ein Bild des Stundenplans anhand der Klassen ID wieder
        /// </summary>
        /// <param name="id">Class ID</param>
        /// <returns>Bild von Stundenplan als Stream</returns>
        public Stream GetClassScheduleById(int id)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            string filepath = $"C:\\projects\\projekt16\\Server\\stundenplan\\{id}.png";
            if(File.Exists(filepath))
            {
                return new FileStream(filepath, FileMode.Open, FileAccess.Read);
            }
            else
            {
                filepath = $"C:\\projects\\projekt16\\Server\\stundenplan\\error.png";
                return new FileStream(filepath, FileMode.Open, FileAccess.Read);
            }
        }

        /// <summary>
        /// Gibt alle Infos von einem User wieder
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User Info</returns>
        public User GetUserById(int id)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return dbConnector.GetUserById(id);
        }

        public User GetUserByName(string name)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return dbConnector.GetUserByName(name);
        }

        /// <summary>
        /// Gibt eine Liste von allen Terminen des Users wieder
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Alle Termine des Users mit ID</returns>
        public List<Appointment> GetAppointmentsByUserId(int id)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            List<Appointment> appointments = dbConnector.GetAppointmentsByUserId(id);
            return appointments;
        }

        /// <summary>
        /// Füge einen neuen Termin hinzu
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="comment"></param>
        /// <param name="date"></param>
        /// <param name="subject"></param>
        /// <param name="grade"></param>
        /// <returns></returns>
        public bool InsertNewUserAppointment(int userId, string name, string comment, DateTime date, string subject, int grade)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return dbConnector.InsertUserAppointment(userId, name, comment, date, subject, grade);
        }


        /// <summary>
        /// Füge einen neuen User in die Datenbank hinzu
        /// </summary>
        /// <param name="firstname">Vorname</param>
        /// <param name="lastname">Nachname</param>
        /// <param name="password">Passwort</param>
        /// <param name="profession_id">Berufsbezeich. ID</param>
        /// <param name="class_id">Klassen ID</param>
        /// <returns>Erfolg true/false</returns>
        public bool InsertUserIntoDb(string firstname, string lastname, string password, int profession_id, int class_id)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");
            return dbConnector.InsertUser(firstname, lastname, password, profession_id, class_id);
        }

        /// <summary>
        /// Gibt wieder, ob die name/passwort Kombination gueltig ist
        /// </summary>
        /// <param name="name">firstname</param>
        /// <param name="password">Passwort</param>
        /// <returns>true/false</returns>
        public bool LoginUser(String name, String password)
        {
            Console.WriteLine($"Anfrage: {System.Reflection.MethodBase.GetCurrentMethod().Name}");

            User user = dbConnector.GetUserByName(name);
            if (user.password == password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gibt eine Liste von Chatnachrichten zurück
        /// </summary>
        /// <param name="amount">Anzahl von Nachrichten, die übergeben werden sollen (0 = Alle)</param>
        /// <returns>Liste der Chatnachrichten</returns>
        public List<String> GetChatMessages(int amount)
        {
            List<String> chat = chatHandler.GetChat();
            if(amount <= 0)
            {
                return chat;
            }

            //Wenn amount >0 gebe nur die aktuellsten >amount< Nachrichten wieder
            chat = new List<String>(chat.Skip(Math.Max(0, chat.Count() - amount)).Take(amount));

            return chat;
        }

        /// <summary>
        /// Schreibe eine Nachricht als User in den Chat
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="message">Nachrricht</param>
        public void WriteToChat(string user, string message)
        {
            chatHandler.AddMessage(user, message);
        }
    }
}