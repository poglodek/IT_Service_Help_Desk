using IT_Service_Help_Desk.Exception;
using ILogger = IT_Service_Help_Desk.Services.IServices.ILogger;

namespace IT_Service_Help_Desk;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public ErrorHandlingMiddleware(ILogger logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotValidException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(ex.Message);
        }
        
        catch (System.Exception ex)
        {
            _logger.LogError(ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("something goes wrong.");
        }
    }
}