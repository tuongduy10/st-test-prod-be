using Microsoft.EntityFrameworkCore;
using WebServer.Domain.Common.Repositories;
using WebServer.Persistence.DbContexts;

namespace WebServer.Infrastructure.Repositories;

public class ProductReadRepository
    : ReadRepository<Product>
    , IProductReadRepository
{
    public ProductReadRepository(ApplicationDbContext db)
        : base(db)
    {
    }

    public async Task<Product?> GetBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default)
    {
        return await Table
            .Include(x => x.Variants)
            .FirstOrDefaultAsync(
                x => x.Slug == slug,
                cancellationToken);
    }

    public override async Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await Table
            .Include(x => x.Variants)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
}
