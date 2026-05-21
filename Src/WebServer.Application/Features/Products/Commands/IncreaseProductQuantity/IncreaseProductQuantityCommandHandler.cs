using WebServer.Domain.Abstractions.Exceptions;
using WebServer.Domain.Common.Repositories;

namespace WebServer.Application.Features.Products.Commands.IncreaseProductQuantity;

public class IncreaseProductQuantityCommandHandler
    : IRequestHandler<IncreaseProductQuantityCommand, Guid>
{
    private readonly IProductReadRepository _readRepository;

    private readonly IProductCommandRepository _commandRepository;

    private readonly IUnitOfWork _uow;

    public IncreaseProductQuantityCommandHandler(
        IProductReadRepository readRepository,
        IProductCommandRepository commandRepository,
        IUnitOfWork uow)
    {
        _readRepository = readRepository;
        _commandRepository = commandRepository;
        _uow = uow;
    }

    public async Task<Guid> Handle(
        IncreaseProductQuantityCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(
            request.ProductId,
            cancellationToken);

        if (product is null)
            throw new NotFoundException("Product not found");

        product.IncreaseStock(
            request.VariantId,
            request.Quantity);

        _commandRepository.Update(product);

        await _uow.SaveChangesAsync(
            cancellationToken);

        return product.Id;
    }
}
