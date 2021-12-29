using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Exception;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.Services.Authentication;
using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Validator;
using JsonConverters;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;


namespace IT_Service_Help_Desk.Services.Services;

public class UserServices : IUserServices
{
    private readonly DatabaseManagement _databaseManagement;
    private readonly DatabaseQueryHelper _databaseQueryHelper;
    private readonly DatabaseHelper _databaseHelper;
    private readonly AuthenticationSettings _authenticationSettings;
    private readonly IRoleServices _roleServices;
    private readonly IValid<RegisterDto> _registerValidator;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public UserServices(DatabaseManagement databaseManagement,
        DatabaseQueryHelper databaseQueryHelper,
        DatabaseHelper databaseHelper,
        AuthenticationSettings authenticationSettings,
        IRoleServices roleServices,
        IValid<RegisterDto> registerValidator,
        IPasswordHasher<User> passwordHasher,
        IMapper mapper)
    {
        _databaseManagement = databaseManagement;
        _databaseQueryHelper = databaseQueryHelper;
        _databaseHelper = databaseHelper;
        _authenticationSettings = authenticationSettings;
        _roleServices = roleServices;
        _registerValidator = registerValidator;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public bool RegisterUser(RegisterDto dto)
    {
        var isValid = _registerValidator.IsValid(dto);
        if(!isValid.Item1)
            throw new NotValidException(isValid.Item2);
        var user = _mapper.Map<User>(dto);
        user.Password = _passwordHasher.HashPassword(user, dto.Password);
        user.Id_role = _roleServices.GetFirstRole().Id;
        return _databaseManagement.InsertObject<User>("users", user);
    }

    public string Login(LoginUserDto userDto)
    {
        var cmd = new MySqlCommand();
        cmd.CommandText = "SELECT * FROM users WHERE email = @email LIMIT 1";
        cmd.Parameters.AddWithValue("@email", userDto.Email);
        var reader = _databaseQueryHelper.SendQuery(cmd);
        var str = _databaseHelper.GetJsonFromReader(reader, false);
        var user = JsonConvert.DeserializeObject<User>(str, new BooleanJsonConverter());
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, userDto.Password) == PasswordVerificationResult.Failed)
            throw new NotFoundException("User not found");
        return GetToken(user);
    }

    private string GetToken(User user)
    {
        var claims = GetClaims(user);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireHours);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    private List<Claim> GetClaims(User user)
    {
        var roleName = _roleServices.GetRoleById(user.Id_role).RoleName;
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, $"{roleName}")
        };
        return claims;
    }
}

