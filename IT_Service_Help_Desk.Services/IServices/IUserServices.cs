using IT_Service_Help_Desk.Dto.User;

namespace IT_Service_Help_Desk.Services.IServices;

public interface IUserServices
{
    bool RegisterUser(RegisterDto dto);
    string Login(LoginUserDto userDto);
    
}