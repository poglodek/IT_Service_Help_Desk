using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IT_Service_Help_Desk.Test.Controller;

public class TicketTypeController_Test : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public TicketTypeController_Test(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public void RegisterUser_InValidRegisterDto_ReturnFalse()
    {
        //Arrange
            
        //Act
        var response = _client.GetAsync("/TicketType").Result;
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
            
    }
}