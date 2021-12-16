using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Database
{
    public class DatabaseConnector
    {
        private const string ConnectionString = @"Server=127.0.0.1;User ID = adminNET; Password=admin;Database=ITHD";

        public bool CanConnectToDataBase()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connected to Database!");
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}
