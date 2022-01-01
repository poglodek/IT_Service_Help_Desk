using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IT_Service_Help_Desk.Test.Controller;

public class TicketController_Test : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    public TicketController_Test(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public void GetTicketById_ValidId_ReturnValidObject()
    {
        //Arrange
        
        //Act
        var response = _client.GetAsync("/Ticket/1").Result;
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Content.ReadAsStream().Length.Should().BeGreaterOrEqualTo(1);

    }
    [Fact]
    public void GetTicketById_InValidId_ReturnNotFoundException()
    {
        //Arrange
        
        //Act
        var response = _client.GetAsync("/Ticket/-5").Result;
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }
    [Fact]
    public void GetTicketById_NotFound_ReturnNotFoundException()
    {
        //Arrange
        
        //Act
        var response = _client.GetAsync("/Ticket/9999").Result;
        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }
}