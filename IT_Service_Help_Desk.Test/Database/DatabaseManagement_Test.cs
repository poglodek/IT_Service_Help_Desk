using Castle.Core.Logging;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Services.Services;
using Moq;
using Newtonsoft.Json;
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
    [Fact]
    public void GetResultFromQueryWithoutLimit_CorrectQuery_ReturnObject()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = management.GetResultFromQuery<Role>($"SELECT * FROM roles;");
        Assert.NotNull(role);

    }
    [Fact]
    public void GetResultsFromQuery_CorrectQuery_ReturnListOfObject()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = management.GetResultsFromQuery<Role>($"SELECT * FROM roles;");
        Assert.NotNull(role);

    }
    [Fact]
    public void InsertObject_CorrectTableAndCorrectObj_ReturnTrue()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role(){RoleName = "Test Roles"};
        var result = management.InsertObject<Role>("roles",role);
        Assert.True(result);

    }
    [Fact]
    public void InsertObject_InCorrectTableAndCorrectObj_ReturnFalse()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role(){RoleName = "Test Roles"};
        var result = management.InsertObject<Role>("ThisTableDoesntExist",role);
        Assert.False(result);

    }
    [Fact]
    public void InsertObject_InCorrectTableAndInCorrectObj_ReturnFalse()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role();
        var result = management.InsertObject<Role>("ThisTableDoesntExist",role);
        Assert.False(result);

    }
    [Fact]
    public void InsertObject_CorrectTableAndInCorrectObj_ReturnFalse()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role();
        var result = management.InsertObject<Role>("roles",role);
        Assert.False(result);

    }

    [Fact]
    public void DeleteObject_CorrectTableAndCorrectObj_ReturnTrue()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role(){RoleName = "Test Roles"};
        var result = management.DeleteObject<Role>("roles",role);
        Assert.True(result);
    }
    [Fact]
    public void DeleteObject_InCorrectTableAndCorrectObj_ReturnFalse()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role(){RoleName = "Test Roles"};
        var result = management.DeleteObject<Role>("ThisTableDoesntExist",role);
        Assert.False(result);
    }
    [Fact]
    public void DeleteObject_InCorrectTableAndInCorrectObj_ReturnFalse()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role(){RoleName = "Test Roles"};
        var result = management.DeleteObject<Role>("ThisTableDoesntExist",role);
        Assert.False(result);
    }
    [Fact]
    public void DeleteObject_CorrectTableAndInCorrectObj_ReturnFalse()
    {
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        var role = new Role(){RoleName = "Test 12Roles"};
        var result = management.DeleteObject<Role>("Roles",role);
        Assert.True(result);
    }

    [Fact]
    public void DeleteObjectById_Correct_ReturnFalse()
    {
        var role = new Role(){RoleName = "Test Roles", Id = 0};
        var management = new DatabaseManagement(new DatabaseConnector(), new Logger());
        management.InsertObject<Role>("roles",role);
        var result = management.DeleteObjectById("Roles",0);
        Assert.True(result);
    }
    
}