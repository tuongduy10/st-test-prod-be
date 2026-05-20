using Microsoft.EntityFrameworkCore;
using WebServer.Domain.Common.Repositories;
using WebServer.Persistence.DbContexts;

namespace WebServer.Infrastructure.Repositories;

public class ReadRepository<TEntity>
    : IReadRepository<TEntity>
    where TEntity : class
{
    protected readonly ApplicationDbContext Db;

    protected readonly DbSet<TEntity> Table;

    public ReadRepository(ApplicationDbContext db)
    {
        Db = db;
        Table = db.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetQueryable()
    {
        return Table.AsQueryable();
    }

    public virtual async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await Table.FindAsync(
            [id],
            cancellationToken);
    }

    public virtual async Task<List<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Table
            .Where(predicate)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Table.AnyAsync(
            predicate,
            cancellationToken);
    }

    public virtual async Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Table.CountAsync(
            predicate,
            cancellationToken);
    }
}
