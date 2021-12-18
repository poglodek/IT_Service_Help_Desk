using Castle.Core.Logging;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Services.Services;
using Moq;
using Xunit;

namespace IT_Service_Help_Desk.Test.Database;

public class DatabaseManagement_Test
{
    [Fact]
    public void GetResultFromQuery_CorrectQuery_ReturnObject()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = management.GetResultFromQuery<Role>($"SELECT * FROM roles LIMIT 1;");
        Assert.NotNull(role);

    }
}