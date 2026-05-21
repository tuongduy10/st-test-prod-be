using WebServer.Domain.Abstractions.Exceptions;
using WebServer.Domain.Common.Repositories;

namespace WebServer.Application.Features.Products.Commands.SoftDeleteProduct;

public class SoftDeleteProductCommandHandler
    : IRequestHandler<SoftDeleteProductCommand, Guid>
{
    private readonly IProductReadRepository _readRepository;

    private readonly IProductCommandRepository _commandRepository;

    private readonly IUnitOfWork _uow;

    public SoftDeleteProductCommandHandler(
        IProductReadRepository readRepository,
        IProductCommandRepository commandRepository,
        IUnitOfWork uow)
    {
        _readRepository = readRepository;
        _commandRepository = commandRepository;
        _uow = uow;
    }

    public async Task<Guid> Handle(
        SoftDeleteProductCommand request,
        CancellationToken cancellationToken)
    {
        var product = await _readRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (product is null)
            throw new NotFoundException("Product not found");

        product.SoftDelete();

        _commandRepository.Update(product);

        await _uow.SaveChangesAsync(
            cancellationToken);

        return product.Id;
    }
}
