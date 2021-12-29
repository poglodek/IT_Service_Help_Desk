using System.Net;
using System.Text;
using FluentAssertions;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.IO.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IT_Service_Help_Desk.Test.Controller;

public class UserController_Test : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly DatabaseManagement _management;

    public UserController_Test(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
        _management = new DatabaseManagement(new DatabaseConnector(),new Logger(), new DatabaseHelper());
    }
    [Fact]
    public void RegisterUser_ValidRegisterDto_ReturnTrue()
    {
        //Arrange
        var model = new RegisterDto()
        {
            Email ="testing@testmail.com", 
            Password = "Password123!@#",
            FirstName = "Test",
            LastName = "Test",
        };
        var json = JsonConvert.SerializeObject(model);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //Act
        var response = _client.PostAsync("/User/register", httpContent).Result;
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        //Cleanup
        var user = new User()
        {
            Email = model.Email
        };
        _management.DeleteObject<User>("users",user);
    }
    [Fact]
    public void RegisterUser_InValidRegisterDto_ReturnFalse()
    {
        //Arrange
        var model = new RegisterDto()
        {
            Email = String.Empty,
            Password = "Password123!@#",
            FirstName = "Test",
            LastName = "Test",
        };
        var json = JsonConvert.SerializeObject(model);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //Act
        var response = _client.PostAsync("/User/register", httpContent).Result;
        //Assert
        response.StatusCode.Should().NotBe(HttpStatusCode.Created);
        
    }
    [Fact]
    public void LoginUser_ValidLoginDto_ReturnTrue()
    {
        //Arrange
        var model = new LoginUserDto()
        {
            Email = "admin@admin.com",
            Password = "test12345",

        };
        var json = JsonConvert.SerializeObject(model);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //Act
        var response = _client.PostAsync("/User/login", httpContent).Result;
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public void LoginUser_InValidLoginDto_ReturnFalse()
    {
        //Arrange
        var model = new LoginUserDto()
        {
            Email = "testing@test.com",
            Password = "notpasswordHere",

        };
        var json = JsonConvert.SerializeObject(model);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //Act
        var response = _client.PostAsync("/User/login", httpContent).Result;
        //Assert
        response.StatusCode.Should().NotBe(HttpStatusCode.OK);
    }
}