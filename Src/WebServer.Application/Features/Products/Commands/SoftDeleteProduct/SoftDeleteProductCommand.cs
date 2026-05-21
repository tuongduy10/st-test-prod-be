using WebServer.Application.Abstractions.Messaging;

namespace WebServer.Application.Features.Products.Commands.SoftDeleteProduct;

public record SoftDeleteProductCommand(
    Guid Id)
    : IRequest<Guid>;
