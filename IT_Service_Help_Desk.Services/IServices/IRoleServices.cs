using IT_Service_Help_Desk.Dto.Role;

namespace IT_Service_Help_Desk.Services.IServices;

public interface IRoleServices
{
    RoleDto GetFirstRole();
    RoleDto GetRoleById(int userIdRole);
}