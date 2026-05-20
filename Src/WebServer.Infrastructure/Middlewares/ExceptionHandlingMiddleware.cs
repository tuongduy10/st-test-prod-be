using Result = WebServer.Domain.Shared.Result;

namespace WebServer.Infrastructure.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            int statusCode = (int)HttpStatusCode.InternalServerError;
            
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            var result = Result.Failure(new Error("InternalServerError", exception.Message));
            string json = JsonConvert.SerializeObject(result);
            await context.Response.WriteAsync(json);
        }
    }
}
