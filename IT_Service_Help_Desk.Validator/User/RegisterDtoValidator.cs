using IT_Service_Help_Desk.Dto;
using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Validator.Helpers;

namespace IT_Service_Help_Desk.Validator.User;

public class RegisterDtoValidator : IValid<RegisterDto>
{
    private readonly EmailValidator _emailValidator;

    public RegisterDtoValidator(EmailValidator emailValidator)
    {
        _emailValidator = emailValidator;
    }
    public (bool, string) IsValid(RegisterDto obj)
    {
        if (!_emailValidator.IsValidEmail(obj.Email))
            return (false,"Email is not valid");
        else if(string.IsNullOrWhiteSpace(obj.FirstName) || obj.FirstName.Length < 3)
            return (false,"First name is not valid");
        else if(string.IsNullOrWhiteSpace(obj.LastName) || obj.LastName.Length < 3)
            return (false,"Last name is not valid");
        else if (obj.Password.Length < 6 || obj.Password.Length > 32 || obj.Password.Length < 5)
            return (false,"Password is not valid");
        return (true,"Model is valid");
        
    }
};
