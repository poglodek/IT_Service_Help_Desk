using AutoMapper;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.Role;
using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Services.Services;
using IT_Service_Help_Desk.Validator;
using IT_Service_Help_Desk.Validator.User;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace IT_Service_Help_Desk.Test.Services;

public class UserServices_Test
{
    private readonly UserServices _userServices;
    private readonly Mock<IRoleServices> _roleServices = new Mock<IRoleServices>();
    private static readonly Mock<ILogger> _logger = new Mock<ILogger>();
    private readonly DatabaseManagement databaseManagment = new DatabaseManagement(new DatabaseConnector(), _logger.Object, new DatabaseHelper());
    private readonly Mock<IValid<RegisterDto>> registerValidator = new Mock<IValid<RegisterDto>>();
    private readonly Mock<IPasswordHasher<User>> passwordHasher = new Mock<IPasswordHasher<User>>();
    private readonly Mock<IMapper> mapper = new Mock<IMapper>();
    public UserServices_Test()
    {
        registerValidator.Setup(x => x.IsValid( It.IsAny<RegisterDto>())).Returns((true, "false"));
        
        var role = new RoleDto(){Id = 1, RoleName = "Admin"};
        _roleServices.Setup(x => x.GetFirstRole()).Returns(role);
        
        passwordHasher.Setup(x=>x.HashPassword(It.IsAny<User>(), It.IsAny<string>())).Returns("password123!@#321");
        
        _userServices = new UserServices(new DatabaseHelper(),new DatabaseConnector(), databaseManagment, _roleServices.Object, registerValidator.Object,
            passwordHasher.Object, mapper.Object);
    }

    
    public void test()
    {
        var user = new RegisterDto()
        {
            Email = "test@test.com",
            Password = "test12345!@#",
            FirstName = "First",
            LastName = "Last"
        };
        var result = _userServices.RegisterUser(user);
        Assert.True(result);
    }
}