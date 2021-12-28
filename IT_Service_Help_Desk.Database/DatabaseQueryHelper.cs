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
            command.Connection.Close();
            return  command.ExecuteReader();
            
        }
        catch
        {
            command.Connection.Close();
            throw new DatabaseQueryException("Cannot send query to database");
        }
        
        return null;
    }
}