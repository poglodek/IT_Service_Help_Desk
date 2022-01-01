
using IT_Service_Help_Desk.Exception;
using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Database;

public class DatabaseQueryHelper
{
    private readonly MySqlConnection _mySqlConnection;
    public DatabaseQueryHelper(DatabaseConnector databaseConnector)
    {
        _mySqlConnection = databaseConnector.GetConnection();
    }

    public MySqlDataReader SendQuery(MySqlCommand command)
    {
        try
        {
            command.Connection = _mySqlConnection;
            command.Connection.Open();
            return command.ExecuteReader();
             



        }
        catch
        {
            command.Connection.Close();
            throw new DatabaseQueryException("Cannot send query to database");
        }
        
        return null;
    }
    public int GetRowsCount(MySqlCommand command)
    {
        try
        {
            command.Connection = _mySqlConnection;
            command.Connection.Open();
            var reader =  command.ExecuteReader();
            var count = 0;
            while (reader.Read())
                ++count;
            command.Connection.Close();
            return count;

        }
        catch
        {
            command.Connection.Close();
            throw new DatabaseQueryException("Cannot send query to database");
        }
        
        return -1;
    }
    
}