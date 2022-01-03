using IT_Service_Help_Desk.Dto.Ticket;
using IT_Service_Help_Desk.Validator.Ticket;
using Xunit;

namespace IT_Service_Help_Desk.Test.Validator.Ticket;

public class CreateTicketDtoValidator_Test
{
    [Fact]
    public void IsValid_ValidObj_ReturnTrue()
    {
        //Arrange
        var validator = new CreateTicketDtoValidator();
        var model = new CreateTicketDto()
        {
            Description = "TestDesc 123",
            Title = "TestTitle 123",
            DateTime = DateTime.Now,
            Id_tickets_status = 1,
            Id_tickets_type = 1,
            Id_user_Created = 1
        };
        //Act
        var result = validator.IsValid(model).Item1;
        //Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("           ")]
    [InlineData(null)]
    public void IsValid_InValidTitle_ReturnFalse(string title)
    {
        //Arrange
        var validator = new CreateTicketDtoValidator();
        var model = new CreateTicketDto()
        {
            Description = "TestDesc 123",
            Title = title,
            DateTime = DateTime.Now,
            Id_tickets_status = 1,
            Id_tickets_type = 1,
            Id_user_Created = 1
        };
        //Act
        var result = validator.IsValid(model).Item1;
        //Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("       ")]
    [InlineData("           ")]
    [InlineData(null)]
    public void IsValid_InValidDescription_ReturnFalse(string description)
    {
        //Arrange
        var validator = new CreateTicketDtoValidator();
        var model = new CreateTicketDto()
        {
            Description = description,
            Title = "title 1231",
            DateTime = DateTime.Now,
            Id_tickets_status = 1,
            Id_tickets_type = 1,
            Id_user_Created = 1
        };
        //Act
        var result = validator.IsValid(model).Item1;
        //Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_InValidDateTimeMinus69eYears_ReturnFalse()
    {
        //Arrange
        var validator = new CreateTicketDtoValidator();
        var model = new CreateTicketDto()
        {
            Description = "description 123123",
            Title = "title 1231",
            DateTime = DateTime.Now.AddYears(-69),
            Id_tickets_status = 1,
            Id_tickets_type = 1,
            Id_user_Created = 1
        };
        //Act
        var result = validator.IsValid(model).Item1;
        //Assert
        Assert.False(result);
    }

    [Fact]
    public void IsValid_InValidDateTimeAdd69eYears_ReturnFalse()
    {
        //Arrange
        var validator = new CreateTicketDtoValidator();
        var model = new CreateTicketDto()
        {
            Description = "description 123123",
            Title = "title 1231",
            DateTime = DateTime.Now.AddYears(69),
            Id_tickets_status = 1,
            Id_tickets_type = 1,
            Id_user_Created = 1
        };
        //Act
        var result = validator.IsValid(model).Item1;
        //Assert
        Assert.False(result);
    }
}