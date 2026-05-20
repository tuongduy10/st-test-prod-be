using WebServer.Domain.Entities;

namespace WebServer.Domain.Common.Repositories;

public interface IProductCommandRepository
    : ICommandRepository<Product>
{
}
