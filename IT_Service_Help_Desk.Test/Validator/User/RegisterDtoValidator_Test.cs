using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Validator.Helpers;
using IT_Service_Help_Desk.Validator.User;
using Xunit;

namespace IT_Service_Help_Desk.Test.Validator.User;

public class RegisterDtoValidator_Test
{
    [Fact]
    public void RegisterDtoValidator_Valid_ShouldReturnTrueAtItem1()
    {
        //Arrange
        var registerDto = new RegisterDto()
        {
            Email = "test@test.com",
            Password = "Test123!@#",
            FirstName = "Test Name",
            LastName = "Test Last Name",
        };
        var validator = new RegisterDtoValidator(new EmailValidator());
        //Act
        var result = validator.IsValid(registerDto).Item1;
        //Assert
        Assert.True(result);
    }
    [Fact]
    public void RegisterDtoValidator_InValidEmail_ShouldReturnFalseAtItem1()
    {
        //Arrange
        var registerDto = new RegisterDto()
        {
            Email = "testtest.com",
            Password = "Test123!@#",
            FirstName = "Test Name",
            LastName = "Test Last Name",
        };
        var validator = new RegisterDtoValidator(new EmailValidator());
        //Act
        var result = validator.IsValid(registerDto).Item1;
        //Assert
        Assert.False(result);
    }
    [Fact]
    public void RegisterDtoValidator_ValidPassword_ShouldReturnFalseAtItem1()
    {
        //Arrange
        var registerDto = new RegisterDto()
        {
            Email = "test@test.com",
            Password = "pass",
            FirstName = "Test Name",
            LastName = "Test Last Name",
        };
        var validator = new RegisterDtoValidator(new EmailValidator());
        //Act
        var result = validator.IsValid(registerDto).Item1;
        //Assert
        Assert.False(result);
    }
    [Fact]
    public void RegisterDtoValidator_InValidFirstName_ShouldReturnFalseAtItem1()
    {
        //Arrange
        var registerDto = new RegisterDto()
        {
            Email = "testtest.com",
            Password = "Test123!@#",
            FirstName = string.Empty,
            LastName = "Test Last Name",
        };
        var validator = new RegisterDtoValidator(new EmailValidator());
        //Act
        var result = validator.IsValid(registerDto).Item1;
        //Assert
        Assert.False(result);
    }
    [Fact]
    public void RegisterDtoValidator_InValidLastName_ShouldReturnFalseAtItem1()
    {
        //Arrange
        var registerDto = new RegisterDto()
        {
            Email = "testtest.com",
            Password = "Test123!@#",
            FirstName = "First Name",
            LastName = String.Empty
        };
        var validator = new RegisterDtoValidator(new EmailValidator());
        //Act
        var result = validator.IsValid(registerDto).Item1;
        //Assert
        Assert.False(result);
    }
    
}