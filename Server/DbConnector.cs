using MySql.Data.MySqlClient;
using Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    /// <summary>
    /// Connector-Klasse für die Anbindung an die Datenbank
    /// </summary>
    class DbConnector
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public DbConnector()
        {
            Initialize();
        }

        /// <summary>
        /// Initialisiert den Connector
        /// </summary>
        private void Initialize()
        {
            server = "localhost";
            database = "sit_project";
            user = "root";
            password = "root";
            string connectionString = $"SERVER={server};DATABASE={database};USER={user};PASSWORD={password};";

            connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Öffne Verbindung zur Datenbank
        /// </summary>
        /// <returns>Erfolg: true/false</returns>
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        /// <summary>
        /// Schließe Verbindung zu Datenbank
        /// </summary>
        /// <returns>Erfolg: true/false</returns>
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Erzeugt eine Datenbankquery mithilfe der übergebenen User-ID
        /// </summary>
        /// <param name="id">ID des gewollten Users</param>
        /// <returns>User-Objekt</returns>
        public User GetUserById(int id)
        {
            string query = $"SELECT user.user_id, user.username, user.first_name, user.last_name, class.class_id, class.bezeichnung as classname, class.stundenplan_id, beruf.beruf_id, beruf.bezeichnung as professionname FROM  user join class on class.class_id = user.class_id join beruf on beruf.beruf_id = user.beruf_id where user.user_id = {id};";

            return GetUser(query);
        }

        /// <summary>
        /// Erzeugt eine Datenbankquery mithilfe der übergebenen User-ID
        /// </summary>
        /// <param name="username">Username des Users</param>
        /// <returns>User-Objekt des Users</returns>
        public User GetUserByName(string username)
        {
            string query = $"SELECT user.user_id, user.username, user.first_name, user.last_name, class.class_id, class.bezeichnung as classname, class.stundenplan_id, beruf.beruf_id, beruf.bezeichnung as professionname FROM  user join class on class.class_id = user.class_id join beruf on beruf.beruf_id = user.beruf_id where user.username = \"{username}\"";

            return GetUser(query);
        }

        /// <summary>
        /// Erstellt ein User-Objekt mit Daten aus der Datenbank anhand der übergebenen Query
        /// </summary>
        /// <param name="query">User-Query</param>
        /// <returns>User Objekt</returns>
        private User GetUser(string query)
        {
            User user = null;

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int uId = (int)dataReader["user_id"];
                    string username = dataReader["username"].ToString();
                    string firstname = dataReader["first_name"].ToString();
                    string lastname = dataReader["last_name"].ToString();
                    int profession_id = (int)dataReader["beruf_id"];
                    string profession_name = dataReader["professionname"].ToString();
                    int class_id = (int)dataReader["class_id"];
                    string class_name = dataReader["classname"].ToString();
                    int stundenplanId = (int)dataReader["stundenplan_id"];

                    user = new User(uId, username, firstname, lastname, new Profession(profession_id, profession_name), new Classroom(class_id, class_name, stundenplanId));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return user;
        }

        /// <summary>
        /// Überprüft, ob die übergebene Username+Passwort Kombination gültig ist
        /// </summary>
        /// <param name="username">Benutzername</param>
        /// <param name="password">Passwort</param>
        /// <returns>true bei validem Username+PW</returns>
        public bool CheckUserLogin(string username, string password)
        {
            bool isValid = false;
            string query = $"Select user.username FROM user WHERE user.username = \"{username}\" AND user.password = \"{password}\"";

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string user = dataReader["username"].ToString();

                    if(user == username)
                    {
                        isValid = true;
                    }
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return isValid;
        }

        /// <summary>
        /// Erstellt eine Liste aller User und deren Info aus der Datenbank
        /// </summary>
        /// <returns>Liste von User-Objekten</returns>
        public List<User> GetAllUsers()
        {
            string query = $"SELECT user.user_id, user.username, user.first_name, user.last_name, class.class_id, class.bezeichnung as classname, class.stundenplan_id, beruf.beruf_id, beruf.bezeichnung as professionname FROM  user join class on class.class_id = user.class_id join beruf on beruf.beruf_id = user.beruf_id;";
            List<User> users = new List<User>();

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int uId = (int)dataReader["user_id"];
                    string username = dataReader["username"].ToString();
                    string firstname = dataReader["first_name"].ToString();
                    string lastname = dataReader["last_name"].ToString();
                    int profession_id = (int)dataReader["beruf_id"];
                    string profession_name = dataReader["professionname"].ToString();
                    int class_id = (int)dataReader["class_id"];
                    string class_name = dataReader["classname"].ToString();
                    int stundenplanId = (int)dataReader["stundenplan_id"];

                    User user = new User(uId, username, firstname, lastname, new Profession(profession_id, profession_name), new Classroom(class_id, class_name, stundenplanId));
                    users.Add(user);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return users;
        }


        /// <summary>
        /// Fügt einen neuen User in die Datenbank ein
        /// </summary>
        /// <param name="username"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="password"></param>
        /// <param name="profession_id"></param>
        /// <param name="class_id"></param>
        /// <returns>Erfolg: true/false</returns>
        public bool InsertUser(string username, string firstname, string lastname, string password, int profession_id, int class_id)
        {
            string query = $"INSERT INTO user (username,first_name,last_name,password, beruf_id, class_id) VALUES(\"{username}\",\"{firstname}\",\"{lastname}\",\"{password}\",\"{profession_id}\", \"{class_id}\"); ";

            return ExecuteQuery(query);
        }

        /// <summary>
        /// Ermittle eine Klasse(Schulklasse) anhand seiner ID und gib diese wieder
        /// </summary>
        /// <param name="id">classroom_Id</param>
        /// <returns>Classroom-Objekt</returns>
        public Classroom GetClassById(int id)
        {
            string query = $"SELECT * FROM class WHERE class_id={id}";
            Classroom classroom = null;

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int classId = (int)dataReader["class_id"];
                    string name = dataReader["bezeichnung"].ToString();
                    int timetable_id = (int)dataReader["stundenplan_id"];

                    classroom = new Classroom(classId, name, timetable_id);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return classroom;
        }

        /// <summary>
        /// Gibt alle in der Datenbank vorhandenen (Schul-)Klassen als Liste wieder
        /// </summary>
        /// <returns>Liste aller Klassen</returns>
        public List<Classroom> GetAllClasses()
        {
            List<Classroom> classes = new List<Classroom>();

            string query = $"SELECT * FROM class";

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    int id = (int)dataReader["class_id"];
                    string name = dataReader["bezeichnung"].ToString();
                    int timetable_id = (int)dataReader["stundenplan_id"];

                    classes.Add(new Classroom(id, name, timetable_id));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Lesen von Datenbank: ${e}");
            }

            CloseConnection();
            return classes;
        }
    
        /// <summary>
        /// Ermittelt ein Beruf aus der Datenbank anhand seiner Id
        /// </summary>
        /// <param name="id">Berufs-Id</param>
        /// <returns>Berufs-Objekt</returns>
        public Profession GetProfessionById(int id)
        {
            string query = $"SELECT * FROM beruf WHERE beruf_id={id}";
            Profession profession = null;

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int berufId = (int)dataReader["beruf_id"];
                    string name = dataReader["bezeichnung"].ToString();

                    profession = new Profession(berufId, name);
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return profession;
        }

        /// <summary>
        /// Gibt alle Berufe aus der Datenbank als Liste wieder
        /// </summary>
        /// <returns>Liste aller Berufe</returns>
        public List<Profession> GetAllProfessions()
        {
            List<Profession> professions = new List<Profession>();

            string query = $"SELECT * FROM beruf";

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    int id = (int)dataReader["beruf_id"];
                    string name = dataReader["bezeichnung"].ToString();

                    professions.Add(new Profession(id, name));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Lesen von Datenbank: ${e}");
            }

            CloseConnection();

            return professions;
        }
        
        /// <summary>
        /// Gibt alle Termine eines Users anhand der User-Id wieder
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Liste aller Termine</returns>
        public List<Appointment> GetAppointmentsByUserId(int id)
        {
            List<Appointment> appointments = new List<Appointment>();

            string query = $"SELECT * FROM termine WHERE user_id = {id}";

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    int app_id = (int)dataReader["termine_id"];
                    string name = dataReader["bezeichnung"].ToString();
                    string subject = dataReader["Fach"].ToString();
                    DateTime date = new DateTime();
                    DateTime.TryParse(dataReader["datum"].ToString(), out date);
                    string comment = dataReader["kommentar"].ToString();
                    int grade = (int)dataReader["Note"];

                    appointments.Add(new Appointment(app_id,id,name,comment,date,subject,grade));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Lesen von Datenbank: ${e}");
            }

            CloseConnection();


            return appointments;
        }

        /// <summary>
        /// Fügt einen neuen Termin in die Datenbank hinzu
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="comment"></param>
        /// <param name="date"></param>
        /// <param name="subject"></param>
        /// <param name="grade"></param>
        /// <returns>Erfolg: true/false</returns>
        public bool InsertUserAppointment(int userId, string name, string comment, DateTime date, string subject, int grade)
        {
            string MySQLFormatDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $"INSERT INTO termine (user_id,bezeichnung,kommentar, datum, Fach, Note) VALUES(\"{userId}\", \"{name}\",\"{comment}\",\"{MySQLFormatDate}\", \"{subject}\", \"{grade}\"); ";
            
            return ExecuteQuery(query);
        }

        /// <summary>
        /// Führt eine Query aus
        /// </summary>
        /// <param name="query">Query string</param>
        /// <returns>Erfolg: true/false</returns>
        private bool ExecuteQuery(string query)
        {
            bool success = true;

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Schreiben auf Datenbank: {e}");
                success = false;
            }

            CloseConnection();
            return success;
        }

        public bool CreateChatMessage(string username, string message)
        {
            DateTime date = DateTime.Now;
            string MySQLFormatDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $"INSERT INTO chatlog (username, message, date) VALUES(\"{username}\",\"{message}\",\"{MySQLFormatDate}\"); ";

            return ExecuteQuery(query);
        }

        public List<String> GetChat()
        {
            List<String> chat = new List<string>();

            string query = $"SELECT * FROM chatlog";

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string username = dataReader["username"].ToString();
                    string message = dataReader["message"].ToString();
                    string date = dataReader["date"].ToString();

                    chat.Add($"{date}|{username}: {message}");
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Lesen von Datenbank: ${e}");
            }

            CloseConnection();

            return chat;
        }
    }
}

