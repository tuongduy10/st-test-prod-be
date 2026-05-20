using WebServer.Domain.Entities;

namespace WebServer.Domain.Common.Repositories;

public interface IProductReadRepository : IReadRepository<Product>
{
    Task<Product?> GetBySlugAsync(
        string slug,
        CancellationToken cancellationToken = default);
}
