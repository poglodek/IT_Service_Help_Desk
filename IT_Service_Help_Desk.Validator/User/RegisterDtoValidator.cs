using IT_Service_Help_Desk.Dto;
using IT_Service_Help_Desk.Dto.User;

namespace IT_Service_Help_Desk.Validator.User;

public class RegisterDtoValidator : RegisterDto, IValid<RegisterDto>
{
    public (bool, string) IsValid(RegisterDto obj)
    {
        
        throw new NotImplementedException();
    }
};
