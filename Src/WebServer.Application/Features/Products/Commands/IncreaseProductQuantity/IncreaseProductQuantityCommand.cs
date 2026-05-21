namespace WebServer.Application.Features.Products.Commands.IncreaseProductQuantity;

public record IncreaseProductQuantityCommand(
    Guid ProductId,
    Guid VariantId,
    int Quantity)
    : IRequest<Guid>;