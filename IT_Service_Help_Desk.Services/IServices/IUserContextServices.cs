using System.Security.Claims;

namespace IT_Service_Help_Desk.Services.IServices;

public interface IUserContextServices
{
    
    ClaimsPrincipal GetUser { get; }

    int GetUserId();
}