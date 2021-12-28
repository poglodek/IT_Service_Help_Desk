namespace IT_Service_Help_Desk.IO.IServices;

public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
}