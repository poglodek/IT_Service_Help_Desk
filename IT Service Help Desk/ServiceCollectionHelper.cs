using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.Services.Services;
using ILogger = IT_Service_Help_Desk.Services.IServices.ILogger;
namespace IT_Service_Help_Desk;

public class ServiceCollectionHelper
{
    public static void AddServices(IServiceCollection serviceCollection)
    {

        serviceCollection.AddScoped<DatabaseConnector>();
        serviceCollection.AddScoped<TableChecker>();
        serviceCollection.AddScoped<TupleHelper>();
        serviceCollection.AddScoped<DatabaseManagement>();
        serviceCollection.AddTransient<ILogger,Logger>();


    }
}