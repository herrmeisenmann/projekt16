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

        internal List<Classroom> GetAllClasses()
        {
            List<Classroom> classes = new List<Classroom>();

            string query = $"SELECT * FROM class";
            User user = null;

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int uId = (int)dataReader["user_id"];
                string firstname = dataReader["first_name"].ToString();
                string lastname = dataReader["last_name"].ToString();
                string password = dataReader["password"].ToString();

                user = new User(uId, firstname, lastname, password);
            }
            CloseConnection();

            return classes;
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
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
        private bool CloseConnection()
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

            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                int uId = (int)dataReader["user_id"];
                string firstname = dataReader["first_name"].ToString();
                string lastname = dataReader["last_name"].ToString();
                string password = dataReader["password"].ToString();

                user = new User(uId, firstname, lastname, password);
            }
            CloseConnection();
            return user;
        }

        public bool InsertUser(User user)
        {

            return true;
        }

        public bool InsertUserAppointment(int id, UserSchedule appointment)
        {


            return true;
        }
    }
}

