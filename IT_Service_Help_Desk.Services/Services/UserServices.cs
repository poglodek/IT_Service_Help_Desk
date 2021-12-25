using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Exception;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Validator;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;

namespace IT_Service_Help_Desk.Services.Services;

public class UserServices : IUserServices
{
    private readonly DatabaseHelper _helper;
    private readonly IValid<RegisterDto> _registerValidator;
    private readonly MySqlConnection _connection;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMapper _mapper;

    public UserServices(DatabaseHelper helper,
        DatabaseConnector connector,
        IValid<RegisterDto> registerValidator,
        IPasswordHasher<User> passwordHasher,
        IMapper mapper)
    {
        _helper = helper;
        _registerValidator = registerValidator;
        _connection = connector.GetConnection();
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public bool RegisterUser(RegisterDto dto)
    {
        var isValid = _registerValidator.IsValid(dto);
        if(!isValid.Item1)
            throw new NotValidException(isValid.Item2);
        //TODO: All 
        return true;
    }
}