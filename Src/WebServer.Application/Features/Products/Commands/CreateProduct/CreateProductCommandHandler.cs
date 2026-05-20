using WebServer.Domain.Common.Repositories;

namespace WebServer.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductCommandRepository _repository;

    private readonly IUnitOfWork _uow;

    public CreateProductCommandHandler(
        IProductCommandRepository repository,
        IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    public async Task<Guid> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Slug,
            request.Description,
            request.Price);

        await _repository.AddAsync(
            product,
            cancellationToken);

        await _uow.SaveChangesAsync(
            cancellationToken);

        return product.Id;
    }
}
