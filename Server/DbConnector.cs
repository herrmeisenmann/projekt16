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
            string query = $"SELECT * FROM user WHERE user_id={id}";
            User user = null;

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int uId = (int)dataReader["user_id"];
                    string firstname = dataReader["first_name"].ToString();
                    string lastname = dataReader["last_name"].ToString();
                    string password = dataReader["password"].ToString();
                    int profession_id = (int)dataReader["beruf_id"];
                    int class_id = (int)dataReader["class_id"];

                    user = new User(uId, firstname, lastname, password, GetProfessionById(profession_id), GetClassById(class_id));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return user;
        }

        public User GetUserByName(string name)
        {
            string query = $"SELECT * FROM user WHERE first_name={name}";
            User user = null;

            List<Profession> professions = GetAllProfessions();
            List<Classroom> classes = GetAllClasses();

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int uId = (int)dataReader["user_id"];
                    string firstname = dataReader["first_name"].ToString();
                    string lastname = dataReader["last_name"].ToString();
                    string password = dataReader["password"].ToString();
                    int profession_id = (int)dataReader["beruf_id"];
                    int class_id = (int)dataReader["class_id"];

                    user = new User(uId, firstname, lastname, password, GetProfessionById(profession_id), GetClassById(class_id));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return user;
        }

        public List<User> GetAllUsers()
        {
            string query = $"SELECT * FROM user";
            List<User> users = new List<User>();
            List<Profession> professions = GetAllProfessions();
            List<Classroom> classes = GetAllClasses();

            OpenConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int id = (int)dataReader["user_id"];
                    string firstname = dataReader["first_name"].ToString();
                    string lastname = dataReader["last_name"].ToString();
                    string password = dataReader["password"].ToString();
                    int profession_id = (int)dataReader["beruf_id"];
                    int class_id = (int)dataReader["class_id"];

                    users.Add(new User(id, firstname, lastname, password, GetProfessionById(profession_id), GetClassById(class_id)));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"Fehler beim Zugriff auf Datenbank: {e}");
            }

            CloseConnection();

            return users;
        }



        public bool InsertUser(string firstname, string lastname, string password, int profession_id, int class_id)
        {
            string query = $"INSERT INTO user (first_name,last_name,password, beruf_id, class_id) VALUES(\"{firstname}\", \"{lastname}\",\"{password}\",\"{profession_id}\", \"{class_id}\"); ";

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

