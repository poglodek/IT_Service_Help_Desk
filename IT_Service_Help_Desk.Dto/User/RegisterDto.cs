namespace IT_Service_Help_Desk.Dto.User;

public class RegisterDto: BaseDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}