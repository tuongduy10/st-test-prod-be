using WebServer.Domain.Abstractions.Exceptions;
using WebServer.Domain.Common.Repositories;

namespace WebServer.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler
    : IRequestHandler<UpdateProductCommand, Guid>
{
    private readonly IProductReadRepository _readRepository;

    private readonly IProductCommandRepository _commandRepository;

    private readonly IUnitOfWork _uow;

    public UpdateProductCommandHandler(
        IProductReadRepository readRepository,
        IProductCommandRepository commandRepository,
        IUnitOfWork uow)
    {
        _readRepository = readRepository;
        _commandRepository = commandRepository;
        _uow = uow;
    }

    public async Task<Guid> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (product is null)
            throw new NotFoundException("Product not found");

        product.Update(
            request.Name,
            request.Description,
            request.Price);

        _commandRepository.Update(product);

        await _uow.SaveChangesAsync(
            cancellationToken);

        return product.Id;
    }
}