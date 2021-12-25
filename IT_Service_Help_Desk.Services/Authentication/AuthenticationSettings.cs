namespace IT_Service_Help_Desk.Services.Authentication;

public class AuthenticationSettings
{
    public string JwtKey { get; set; }
    public int JwtExpireHours { get; set; }
    public string JwtIssuer { get; set; }
}