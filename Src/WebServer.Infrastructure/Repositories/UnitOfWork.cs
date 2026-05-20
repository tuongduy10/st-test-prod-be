using WebServer.Domain.Common.Repositories;
using WebServer.Persistence.DbContexts;

namespace WebServer.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _db.SaveChangesAsync(
            cancellationToken);
    }
}

