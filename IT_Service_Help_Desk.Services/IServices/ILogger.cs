namespace IT_Service_Help_Desk.Services.IServices;

public interface ILogger
{
    void LogInfo(string message);
    void LogError(string message);
}