using Microsoft.EntityFrameworkCore;
using WebServer.Domain.Common.Repositories;
using WebServer.Persistence.DbContexts;

namespace WebServer.Infrastructure.Repositories;

public class ProductCommandRepository
    : CommandRepository<Product>
    , IProductCommandRepository
{
    public ProductCommandRepository(ApplicationDbContext db)
        : base(db)
    {
    }
}
