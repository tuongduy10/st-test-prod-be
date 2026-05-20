using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

namespace WebServer.Api.Extensions;

public static class ApiExtension
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(
            options =>
            {
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        return services;
    }

    public static IResult Result<T>(this T response)
        where T : Result
    {
        return response.IsSuccess ? Results.Ok(response) : Results.BadRequest(response);
    }
}
