namespace IT_Service_Help_Desk.Exception;

public class NotFoundException : System.Exception
{
    public NotFoundException(string userNotFound) : base(userNotFound)
    {
        
    }
}