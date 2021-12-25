using IT_Service_Help_Desk.Validator.Helpers;
using Xunit;

namespace IT_Service_Help_Desk.Test.Validator.Helpers;

public class EmailValidator_Test
{
    [Fact]
    public void IsValidEmail_ValidEmail_ReturnsTrue()
    {
        // Arrange
        var emailValidator = new EmailValidator();
        var email = "test@test.com";
        // Act
        var result = emailValidator.IsValidEmail(email);
        // Assert
        Assert.True(result);
    }
    [Theory]
    [InlineData("test!test.com")]
    [InlineData("test@")]
    [InlineData("@test@")]
    [InlineData("est@@")]
    [InlineData("@test.com")]
    public void IsValidEmail_InvalidEmail_ReturnsFalse(string email)
    {
        // Arrange
        var emailValidator = new EmailValidator();
        // Act
        var result = emailValidator.IsValidEmail(email);
        // Assert
        Assert.False(result);
    }
}