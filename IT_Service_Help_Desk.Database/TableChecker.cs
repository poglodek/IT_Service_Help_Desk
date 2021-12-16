using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Database
{
    public class TableChecker
    {
        private readonly MySqlConnection _connection;

        private List<string> tables = new List<string>
        {
            "Roles","Users", "Tickets", "Tickets_Status", "Tickets_Comments"
        };

        public TableChecker(DatabaseConnector database)
        {
            _connection = database.GetConnection();
        }

        public bool IsTable()
        {
           
            foreach (var table in tables)
            {
                _connection.Open();
                var query = $"SELECT * FROM information_schema.tables WHERE table_schema = 'ITHD' AND table_name = '{table}' LIMIT 1;";
                var cmd = new MySqlCommand(query, _connection);
                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();
                    CreateTable(table);
                }
                   
               
                _connection.Close();
            }

            return true;
        }

        private void CreateTable(string tableName)
        {
            Console.WriteLine($"Creating Table {tableName}");

            var query = $"CREATE TABLE {tableName}(id INT(32) UNSIGNED AUTO_INCREMENT PRIMARY KEY)";
            var cmd = new MySqlCommand(query, _connection);
            var reader = cmd.ExecuteReader();
            reader.Close();

        }
    }
}
