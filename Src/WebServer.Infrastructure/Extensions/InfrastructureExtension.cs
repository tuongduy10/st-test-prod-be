using WebServer.Domain.Common.Repositories;
using WebServer.Infrastructure.Repositories;

namespace WebServer.Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
        services.AddTransient<ExceptionHandlingMiddleware>();

        services.AddScoped<
            IUnitOfWork,
            UnitOfWork>();

        services.AddScoped<
            IProductReadRepository,
            ProductReadRepository>();

        services.AddScoped<
            IProductCommandRepository,
            ProductCommandRepository>();

        return services;
    }
}
