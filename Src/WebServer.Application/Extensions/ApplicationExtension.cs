using WebServer.Domain.Common.Repositories;

namespace WebServer.Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services)
    {
        var assembly = AssemblyReference.Assembly;

        var handlerRegistrations = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>))
                .Select(i => (Implementation: t, ServiceType: i)));

        foreach (var (impl, service) in handlerRegistrations)
            services.AddScoped(service, impl);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavivor<,>));
        services.AddScoped<ISender, Sender>();
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}
