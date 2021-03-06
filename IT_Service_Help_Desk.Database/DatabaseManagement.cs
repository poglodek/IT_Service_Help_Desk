using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.IO.IServices;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace IT_Service_Help_Desk.Database;

public class DatabaseManagement
{
    private readonly ILogger _logger;
    private readonly DatabaseHelper _baseHelper;
    private readonly MySqlConnection _mySqlConnection;

    public DatabaseManagement(DatabaseConnector databaseConnector,
        ILogger logger,
        DatabaseHelper baseHelper)
    {
        _logger = logger;
        _baseHelper = baseHelper;
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
            _mySqlConnection.Close();
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
            var json = _baseHelper.GetJsonFromReader(reader);
            reader.Close();
            _mySqlConnection.Close();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
        }
        catch
        {
            _mySqlConnection.Close();
            _logger.LogError($"Error in database query: {query}");
            return null;
        }
    }

    public T GetResultFromQuery<T>(string selectCommand) where T : class
    {
        string query = selectCommand;
        if (!selectCommand.ToUpper().Contains("LIMIT 1"))
            query = selectCommand.Replace(";", " LIMIT 1;");

        try
        {
            _mySqlConnection.Open();
            var command = new MySqlCommand(query, _mySqlConnection);
            var reader = command.ExecuteReader();
            var json = _baseHelper.GetJsonFromReader(reader, false);
            reader.Close();
            _mySqlConnection.Close();
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch
        {
            _mySqlConnection.Close();
            _logger.LogError($"Error in database query: {selectCommand}");
            return null;
        }
    }

    public bool InsertObject<T>(string tableName, T t) where T : EntityBase
    {
        string query = String.Empty;
        try
        {
            _mySqlConnection.Open();
            var properties = t.GetType().GetProperties();
            string columns = "(";
            string values = "(";
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var propertyValue = t.GetType().GetProperty(propertyName)?.GetValue(t);
                if (propertyValue is null || propertyName == "Id")
                    continue;
                columns += $"{propertyName},";
                values += $"'{propertyValue}',";
            }

            if (columns.Length < 2)
                throw new System.Exception("No properties to insert");
            query =
                $"INSERT INTO {tableName} {columns.Substring(0, columns.Length - 1)} ) VALUES {values.Substring(0, values.Length - 1)} );";
            var cmd = new MySqlCommand(query, _mySqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Close();
            _mySqlConnection.Close();
            return true;
        }
        catch
        {
            _mySqlConnection.Close();
            _logger.LogError($"Error in database table:{tableName}, query: {query}");
            return false;
        }
    }

    public bool DeleteObject<T>(string tableName, T t) where T : EntityBase
    {
        try
        {
            _mySqlConnection.Open();
            var properties = t.GetType().GetProperties();
            string where = "";
            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var propertyValue = t.GetType().GetProperty(propertyName)?.GetValue(t);
                if (propertyValue is null || propertyName.ToUpper().Contains("ID") && propertyValue.ToString() == "0" || propertyName.ToUpper().Contains("IS") || propertyName.ToUpper().Contains("DATE"))
                    continue;
                where += $"{propertyName} = '{propertyValue}' AND ";
            }

            if (where.Length < 2)
                throw new System.Exception("No properties to delete");
            string query = $"DELETE FROM {tableName} WHERE {where.Substring(0, where.Length - 4)} LIMIT 1;";
            var cmd = new MySqlCommand(query, _mySqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Close();
            _mySqlConnection.Close();
            return true;
        }
        catch
        {
            _mySqlConnection.Close();
            _logger.LogError($"Error in database query: {tableName}");
            return false;
        }
    }

    public bool DeleteObjectById(string tableName, int id)
    {
        try
        {
            _mySqlConnection.Open();
            string query = $"DELETE FROM {tableName} WHERE Id = {id} LIMIT 1;";
            var cmd = new MySqlCommand(query, _mySqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Close();
            _mySqlConnection.Close();
            return true;
        }
        catch
        {
            _mySqlConnection.Close();
            _logger.LogError($"Error in database query: {tableName}");
            return false;
        }
    }

    public bool UpdateObject<T>(string tableName, T obj, int id = -1) where T : EntityBase
    {
        try
        {
            _mySqlConnection.Open();
            var properties = obj.GetType().GetProperties();
            string update = string.Empty;

            foreach (var property in properties)
            {
                string propertyName = property.Name;
                var propertyValue = obj.GetType().GetProperty(propertyName)?.GetValue(obj);
                if (propertyValue is null || propertyName == "Id")
                    continue;
                update += $"{propertyName} = '{propertyValue}', ";
            }

            var entityId = id == -1 ? obj.Id : id;
            string query = $"UPDATE {tableName} SET {update.Substring(0, update.Length - 2)} WHERE Id = {entityId};";
            var cmd = new MySqlCommand(query, _mySqlConnection);
            var reader = cmd.ExecuteReader();
            reader.Close();
            _mySqlConnection.Close();
            return true;
        }
        catch
        {
            _mySqlConnection.Close();
            _logger.LogError($"Error in database query: {tableName}");
            return false;
        }
    }
}