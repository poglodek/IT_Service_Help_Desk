using IT_Service_Help_Desk.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DatabaseConnector>();
builder.Services.AddScoped<TableChecker>();

var app = builder.Build();
var scope = app.Services.CreateScope();
if (!scope.ServiceProvider.GetService<DatabaseConnector>().CanConnectToDataBase())
    app.StopAsync();
scope.ServiceProvider.GetService<TableChecker>().IsTable();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
