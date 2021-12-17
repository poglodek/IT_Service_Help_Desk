using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IT_Service_Help_Desk.Helpers;
using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Database
{
    public class TableChecker
    {
        private readonly TupleHelper _tupleHelper;
        private readonly MySqlConnection _connection;

        private string[] tables = new string[]
        {
            "Roles","Users", "Tickets", "Tickets_Status", "Tickets_Comments"
        };


        //TableName, ColumnName, Column definition OR FOREIGN KEY Column Name, FOREIGN KEY Table Name OR NULL if column dont have  FOREIGN KEY
        private List<(string, string, string, string)> Columns = new List<(string, string, string, string)>()
        {
            ("Users","FirstName", "varchar(20)", null)!, 
            ("Users","LastName", "varchar(20)",null)!, 
            ("Users","Email", "varchar(32)",null)!, 
            ("Users","Password", "varchar(513)",null)!,
            ("Roles","Id_role", "Id", "Users"),
        };

        public TableChecker(DatabaseConnector database, TupleHelper tupleHelper)
        {
            _tupleHelper = tupleHelper;
            _connection = database.GetConnection();
        }

        public bool IsTable()
        {
            _connection.Open();
            foreach (var table in tables)
            {
               
                var query = $"SELECT * FROM information_schema.tables WHERE table_schema = 'ITHD' AND table_name = '{table}' LIMIT 1;";
                var cmd = new MySqlCommand(query, _connection);
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    CreateTable(table);
                }
                else
                    reader.Close();
            }
            _connection.Close();
            SetTableColumns();
            return true;
        }

        public void CreateTable(string tableName)
        {
            var query = $"CREATE TABLE {tableName}(Id INT(32) UNSIGNED AUTO_INCREMENT PRIMARY KEY)";
            var cmd = new MySqlCommand(query, _connection);
            var reader = cmd.ExecuteReader();
            reader.Close();
        }

        private void SetTableColumns()
        {
            _connection.Open();
            foreach (var table in tables)
            {
                var query = $"SHOW COLUMNS FROM {table};";
                var cmd = new MySqlCommand(query, _connection);
                var reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    var column = reader.GetString("Field");

                    var tuple = Columns.FirstOrDefault(x =>
                        x.Item1.ToUpper() == table.ToUpper() && x.Item2.ToUpper() == column.ToUpper());
                    if (tuple.Item1 is not null)
                    {
                        Columns.Remove(tuple);
                        //Columns.Add(_tupleHelper.CreateTuple(tuple.Item1, tuple.Item2, tuple.Item3, true));
                    }

                }
                 
                reader.Close();
            }

            CreateColumns();
            _connection.Close();
        }

        private void CreateColumns()
        {
            foreach (var column in Columns)
            {
                string query = "ALTER TABLE ";
                if (column.Item4 is null)
                    query += $"{column.Item1} ADD {column.Item2} {column.Item3};";
                else
                    query += $"{column.Item1} ADD CONSTRAINT {column.Item2} FOREIGN KEY ({column.Item3})  REFERENCES {column.Item4} ({column.Item3});"; 
                var cmd = new MySqlCommand(query, _connection);
                var reader = cmd.ExecuteReader();
                reader.Close();
            }
        }
            
    }
}
