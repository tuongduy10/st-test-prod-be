namespace WebServer.Application.Features.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Slug,
    string Description,
    decimal Price)
    : IRequest<Guid>;
