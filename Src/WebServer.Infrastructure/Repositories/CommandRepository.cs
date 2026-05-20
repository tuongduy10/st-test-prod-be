using Microsoft.EntityFrameworkCore;
using WebServer.Domain.Common.Repositories;
using WebServer.Persistence.DbContexts;

namespace WebServer.Infrastructure.Repositories;

public class CommandRepository<TEntity>
    : ICommandRepository<TEntity>
    where TEntity : class
{
    protected readonly ApplicationDbContext Db;

    protected readonly DbSet<TEntity> Table;

    public CommandRepository(ApplicationDbContext db)
    {
        Db = db;
        Table = db.Set<TEntity>();
    }

    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        await Table.AddAsync(
            entity,
            cancellationToken);
    }

    public virtual async Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        await Table.AddRangeAsync(
            entities,
            cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        Table.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        Table.Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<TEntity> entities)
    {
        Table.RemoveRange(entities);
    }
}
