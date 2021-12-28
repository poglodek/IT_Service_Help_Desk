using System.Text;
using IT_Service_Help_Desk;
using IT_Service_Help_Desk.Database;
using IT_Service_Help_Desk.Database.Entity;
using IT_Service_Help_Desk.Helpers;
using IT_Service_Help_Desk.Services.Authentication;
using IT_Service_Help_Desk.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ILogger = IT_Service_Help_Desk.IO.IServices.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
var authSettings = new AuthenticationSettings();

builder.Configuration.GetSection("Authentication").Bind(authSettings);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Bearer";
        options.DefaultScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(config =>
    {
        config.SaveToken = true;
        config.RequireHttpsMetadata = false;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authSettings.JwtIssuer,
            ValidAudience = authSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtKey)
            )
        };
    });

builder.Services.AddSingleton(authSettings);
ServiceCollectionHelper.AddServices(builder.Services);
var app = builder.Build();
var scope = app.Services.CreateScope();
if (!scope.ServiceProvider.GetService<DatabaseConnector>().CanConnectToDataBase())
    app.StopAsync();
scope.ServiceProvider.GetService<TableChecker>().IsTable();
scope.ServiceProvider.GetService<ILogger>().LogInfo("Test");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
