namespace WebServer.Service.Extensions;

public static class WebServerExtension
{
    private const string AllowLocalClient = "AllowLocalClient";
    private const string AllowProductionClient = "AllowProductionClient";

    public static IServiceCollection AddWebServerConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        // CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy(AllowLocalClient, policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            options.AddPolicy(AllowProductionClient, policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // Swagger
        services.AddSwaggerGenConfiguration();

        // Json serialize
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        };

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddCarter();

        services.AddPersistenceConfiguration(configuration);
        services.AddApiConfiguration();
        services.AddInfrastructureConfiguration(configuration);
        services.AddApplicationConfiguration();
        return services;
    }

    public static void UseWebServerConfiguration(this WebApplication app)
    {
        app.UseCors(app.Environment.IsDevelopment() ? AllowLocalClient : AllowProductionClient);

        app.MapCarter();

        app.Services.ApplyMigrations();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    private static IServiceCollection AddSwaggerGenConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
        });
        return services;
    }
}
