using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace Client.Repository
{
    public class ClientRepo
    { 
        public void selectUser()
        {
            string myConnectionString = "SERVER=localhost;" +
                                        "DATABASE=SIT;" +
                                        "UID=root;" +
                                        "PASSWORD=niki2694;";

            MySqlConnection connection = new MySqlConnection(myConnectionString);
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM test";
            MySqlDataReader Reader;
            connection.Open();
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string row = "";
                for (int i = 0; i < Reader.FieldCount; i++)
                    row += Reader.GetValue(i).ToString() + ", ";
                Console.WriteLine(row);
            }
            connection.Close();
        }
    }
}