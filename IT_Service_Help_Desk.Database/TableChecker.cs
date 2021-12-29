using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Database
{
    public class TableChecker
    {
        private readonly MySqlConnection _connection;

        private string[] tables = new string[]
        {
            "Roles","Users", "Tickets", "Tickets_Status", "Tickets_Comments", "Tickets_Type"
        };


        //TableName, ColumnName, Column definition OR FOREIGN KEY Column Name, FOREIGN KEY Table Name OR NULL if column dont have  FOREIGN KEY
        private List<(string, string, string, string)> Columns = new()
        {

            //Create first column without references 
            //Users
            ("Users", "Id", "int(32)", null)!,
            ("Users", "FirstName", "varchar(20)", null)!,
            ("Users", "LastName", "varchar(20)", null)!,
            ("Users", "Email", "varchar(32) UNIQUE", null)!,
            ("Users", "Password", "varchar(513)", null)!,
            ("Users", "IsEnabled", "bit(1)", null)!,

            //Roles
            ("Roles", "Id", "int(32)", null)!,
            ("Roles", "RoleName", "varchar(20)", null)!,

            //Tickets
            ("Tickets", "Id", "int(32)", null)!,
            ("Tickets", "Title", "varchar(32) UNIQUE", null)!,
            ("Tickets", "Description", "varchar(255)", null)!,
            ("Tickets", "DateTime", "datetime", null)!,

            //Tickets_Type
            ("Tickets_Type", "Id", "int(32)", null)!,
            ("Tickets_Type", "TypeName", "varchar(32)", null)!,

            //Tickets_Comments
            ("Tickets_Comments", "Id", "int(32)", null)!,
            ("Tickets_Comments", "Comment", "varchar(32)", null)!,
            ("Tickets_Comments", "DateTime", "datetime", null)!,

            //Tickets_Status
            ("Tickets_Status", "Id", "int(32)", null)!,
            ("Tickets_Status", "Status", "varchar(32)", null)!,

            //references here!
            ("Users", "Id_role", "Id", "Roles"),
            ("Tickets", "Id_user_Created", "Id", "Users"),
            ("Tickets", "Id_tickets_status", "Id", "Tickets_Status"),
            ("Tickets", "Id_tickets_type", "Id", "Tickets_Type"),
            ("Tickets_Comments", "Id_user", "Id", "Users"),
            ("Tickets_Comments", "Id_Tickets", "Id", "Tickets"),
        };

        private readonly List<(string, string, string, string)> _columnsExist;
        public TableChecker(DatabaseConnector database)
        {
            _connection = database.GetConnection();
            _columnsExist = new List<(string, string, string, string)>(Columns);
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

                while (reader.Read())
                {
                    var column = reader.GetString("Field");
                    var tuple = Columns.FirstOrDefault(x =>
                        x.Item1.ToUpper() == table.ToUpper() && x.Item2.ToUpper() == column.ToUpper());
                    if (tuple.Item1 is not null)
                    {
                        Columns.Remove(tuple);
                    }
                }
                reader.Close();
            }
            PreapartColumns();
            _connection.Close();
        }

        private void PreapartColumns()
        {
            foreach (var column in Columns)
            {
                string query = "ALTER TABLE ";
                if (column.Item4 is null)
                    query += $"{column.Item1} ADD {column.Item2} {column.Item3};";
                else
                {
                    var primaryKeyField = _columnsExist.FirstOrDefault(x => x.Item1 == column.Item4 && x.Item2 == column.Item3);
                    var columnType = $"ALTER TABLE {column.Item1} ADD {column.Item2} {primaryKeyField.Item3} UNSIGNED NOT NULL;";
                    ExecuteSqlCommand(columnType);
                    query += $"{column.Item1} ADD CONSTRAINT {column.Item2}_fk FOREIGN KEY ({column.Item2})  REFERENCES {column.Item4}({column.Item3});";
                }
                ExecuteSqlCommand(query);

            }
        }

        private void ExecuteSqlCommand(string query)
        {
            var cmd = new MySqlCommand(query, _connection);
            var reader = cmd.ExecuteReader();
            reader.Close();
        }

    }
}
