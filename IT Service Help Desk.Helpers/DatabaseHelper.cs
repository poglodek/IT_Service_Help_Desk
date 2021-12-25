using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Helpers;

public class DatabaseHelper
{ 
    public string GetJsonFromReader(MySqlDataReader reader, bool hasManyRows = true)
    {
        string sqlText = @"{";
        if (hasManyRows)
            sqlText = @"[";
        while (reader.Read())
        {
            sqlText += hasManyRows ? "{" : "";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                sqlText += $"\"{reader.GetName(i)}\": \"{reader[i]}\",";
            }
            sqlText += hasManyRows ? "}," : "";
        }
        int lastIndex = hasManyRows ? sqlText.Length - 3 : sqlText.Length - 1;
        return sqlText.Substring(0, lastIndex) + (hasManyRows ? "}]" : @"}");
    }
}