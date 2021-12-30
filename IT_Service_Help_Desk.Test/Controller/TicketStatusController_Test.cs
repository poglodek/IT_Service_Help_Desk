using System.Net;
using System.Text;
using FluentAssertions;
using IT_Service_Help_Desk.Dto.User;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace IT_Service_Help_Desk.Test.Controller;

public class TicketStatusController_Test : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public TicketStatusController_Test(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public void RegisterUser_InValidRegisterDto_ReturnFalse()
    {
        //Arrange
        
        //Act
        var response = _client.GetAsync("/TicketStatus").Result;
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
    }
}