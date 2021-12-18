using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Database;

public class DatabaseManagement
{
    private readonly MySqlConnection _mySqlConnection;

    public DatabaseManagement(DatabaseConnector databaseConnector)
    {
        _mySqlConnection = databaseConnector.GetConnection();
    }

    public bool SendSqlCommand(string query)
    {
        try
        {
            _mySqlConnection.Open();
            var command = new MySqlCommand(query, _mySqlConnection);
            var reader = command.ExecuteReader();
            reader.Close();
            _mySqlConnection.Close();
            return true;
        }
        catch 
        {
            Console.WriteLine("Error");
            return false;
        }
       
    }
    
}