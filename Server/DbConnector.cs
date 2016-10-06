using MySql.Data.MySqlClient;
using Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server
{
    class DbConnector
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;

        public DbConnector()
        {
            Initialize();
        }
        private void Initialize()
        {
            server = "localhost";
            database = "sit_project";
            user = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "user=" + user + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
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

        //Close connection
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
        public User GetUserById(int id)
        {
            string query = $"SELECT user.user_id, user.username, user.first_name, user.last_name, class.class_id, class.bezeichnung as classname, class.stundenplan_id, beruf.beruf_id, beruf.bezeichnung as professionname FROM  user join class on class.class_id = user.class_id join beruf on beruf.beruf_id = user.beruf_id where user.user_id = {id};";

            return GetUser(query);
        }

        public User GetUserByName(string username)
        {
            string query = $"SELECT user.user_id, user.username, user.first_name, user.last_name, class.class_id, class.bezeichnung as classname, class.stundenplan_id, beruf.beruf_id, beruf.bezeichnung as professionname FROM  user join class on class.class_id = user.class_id join beruf on beruf.beruf_id = user.beruf_id where user.username = \"{username}\"";

            return GetUser(query);
        }

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



        public bool InsertUser(string username, string firstname, string lastname, string password, int profession_id, int class_id)
        {
            string query = $"INSERT INTO user (username,first_name,last_name,password, beruf_id, class_id) VALUES(\"{username}\",\"{firstname}\",\"{lastname}\",\"{password}\",\"{profession_id}\", \"{class_id}\"); ";

            return ExecuteQuery(query);
        }

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

        public bool InsertUserAppointment(int userId, string name, string comment, DateTime date, string subject, int grade)
        {
            string MySQLFormatDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            string query = $"INSERT INTO termine (user_id,bezeichnung,kommentar, datum, Fach, Note) VALUES(\"{userId}\", \"{name}\",\"{comment}\",\"{MySQLFormatDate}\", \"{subject}\", \"{grade}\"); ";
            
            return ExecuteQuery(query);
        }

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
    }
}

