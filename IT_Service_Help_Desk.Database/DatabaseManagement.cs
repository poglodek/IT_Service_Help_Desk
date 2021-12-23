using IT_Service_Help_Desk.Services.IServices;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace IT_Service_Help_Desk.Database;

public class DatabaseManagement
{
    private readonly ILogger _logger;
    private readonly MySqlConnection _mySqlConnection;

    public DatabaseManagement(DatabaseConnector databaseConnector, ILogger logger)
    {
        _logger = logger;
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
            _logger.LogError($"Error in database query: {query}");
            return false;
        }
       
    }
    public IEnumerable<T>? GetResultsFromQuery<T>(string query)  
    {
        try
        {
            _mySqlConnection.Open();
            var command = new MySqlCommand(query, _mySqlConnection);
            var reader = command.ExecuteReader();
            string sqlText = @"[";
            while (reader.Read())
            {
                sqlText += "{";
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    sqlText += $"\"{reader.GetName(i)}\": \"{reader[i]}\",";
                }
                sqlText += "},";
            }
            var json = sqlText.Substring(0, sqlText.Length - 3);
            json += "}]";
            reader.Close();
            _mySqlConnection.Close();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }
        catch 
        {
            _logger.LogError($"Error in database query: {query}");
            return null;
        }
       
    }
    public T GetResultFromQuery<T>(string selectCommand) where T : class
    {
        string query = selectCommand;
        if(!selectCommand.ToUpper().Contains("LIMIT 1"))
            query = selectCommand.Replace(";", " LIMIT 1;");

        try
        {
            _mySqlConnection.Open();
            var command = new MySqlCommand(query, _mySqlConnection);
            var reader = command.ExecuteReader();
            string sqlText = @"{";
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                   sqlText += $"\"{reader.GetName(i)}\": \"{reader[i]}\",";
                }
            }
            var json = sqlText.Substring(0, sqlText.Length - 1);
            json += "}";
            reader.Close();
            _mySqlConnection.Close();
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch 
        {
            _logger.LogError($"Error in database query: {selectCommand}");
            return null;
        }
       
    }
    
}