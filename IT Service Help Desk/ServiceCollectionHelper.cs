using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Dto;
using IT_Service_Help_Desk.Dto.User;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.IO.Services;
using IT_Service_Help_Desk.Services.IServices;
using IT_Service_Help_Desk.Services.Services;
using IT_Service_Help_Desk.Validator;
using IT_Service_Help_Desk.Validator.Helpers;
using IT_Service_Help_Desk.Validator.User;
using JsonConverters;
using Microsoft.AspNetCore.Identity;
using ILogger = IT_Service_Help_Desk.IO.IServices.ILogger;
namespace IT_Service_Help_Desk;

public class ServiceCollectionHelper
{
    public static void AddServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ErrorHandlingMiddleware>();
        serviceCollection.AddScoped<IUserServices, UserServices>();
        serviceCollection.AddScoped<IRoleServices, RoleServices>();
        serviceCollection.AddScoped<ITicketServices, TicketServices>();
        serviceCollection.AddScoped<ITicketStatusServices, TicketStatusServices>();
        serviceCollection.AddScoped<ITicketTypeServices, TicketTypeServices>();
        serviceCollection.AddAutoMapper(typeof(HelpDeskMapper).Assembly);
        serviceCollection.AddScoped<DatabaseConnector>();
        serviceCollection.AddScoped<TableChecker>();
        serviceCollection.AddScoped<TupleHelper>();
        serviceCollection.AddScoped<DatabaseQueryHelper>();
        serviceCollection.AddScoped<DatabaseHelper>();
        serviceCollection.AddScoped<DatabaseManagement>();
        serviceCollection.AddScoped<EmailValidator>();
        serviceCollection.AddScoped<IValid<RegisterDto>,RegisterDtoValidator>();
        serviceCollection.AddTransient<ILogger,Logger>();

    }
}