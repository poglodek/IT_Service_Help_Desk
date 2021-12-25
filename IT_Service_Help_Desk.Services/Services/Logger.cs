using System.Reflection;
using IT_Service_Help_Desk.Services.IServices;

namespace IT_Service_Help_Desk.Services.Services;

public class Logger : ILogger
{
    private readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;
    private string LogFilePath;
    private string LogFileName = "Log-";
    private string LogFileExtension = ".LOG";

    public Logger()
    {
        LogFilePath = AppPath + @"Logs\";
    }
    public void LogInfo(string message)
    {
        Log(message, "|INFO|");
    }
    public void LogError(string message)
    {
        Log(message, "|ERROR|");
    }
    private void Log(string message, string prefix)
    {
        WriteToFile($"{DateTime.Now.ToString("g")} {prefix} {message}");
    }
    private bool CheckIfDirectoryExists()
    {
        return Directory.Exists(LogFilePath);
    }
    private void CreateDirectory()
    {
        Directory.CreateDirectory(LogFilePath);
    }
    private bool CheckIfFileExists()
    {
        return File.Exists(LogFilePath + LogFileName  + DateTime.Now.ToString("yyyy-MM-dd") +  LogFileExtension);
    }
    private void CreateFile()
    {
        File.Create(LogFilePath + LogFileName + DateTime.Now.ToString("yyyy-MM-dd") + LogFileExtension);
    }
    private void WriteToFile(string message)
    {
        try
        {
            if(!CheckIfDirectoryExists())
                CreateDirectory();
            if(CheckIfFileExists())
                CreateFile();
            using StreamWriter sw = File.AppendText(LogFilePath + LogFileName + DateTime.Now.ToString("yyyy-MM-dd") + LogFileExtension);
            sw.WriteLine(message);
        }catch
        {
            Console.WriteLine(message);
            Console.WriteLine("Error writing to file!");
        }
       
    }
    
}