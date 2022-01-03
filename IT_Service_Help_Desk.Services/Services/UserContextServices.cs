using System.Security.Claims;
using IT_Service_Help_Desk.Exception;
using IT_Service_Help_Desk.Services.IServices;
using Microsoft.AspNetCore.Http;

namespace IT_Service_Help_Desk.Services.Services;

public class UserContextServices : IUserContextServices
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextServices(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal GetUser => _httpContextAccessor.HttpContext?.User;

    public int GetUserId()
    {
        if (GetUser is null) return -1;
        try
        {
            return int.Parse(GetUser.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
        catch
        {
            throw new NotFoundException("User not found.");
        }
    }
}