namespace WebServer.Persistence.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistenceConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string conn = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<ApplicationDbContext>(
            option => option.UseNpgsql(conn,
                action => action.MigrationsHistoryTable(HistoryRepository.DefaultTableName, TblSchemaConstant.Default)));
       
        return services;
    }

    public static void ApplyMigrations(this IServiceProvider provider)
    {
        //using var scope = provider.CreateScope();
        //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //context.Database.Migrate();
    }
}
