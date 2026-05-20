using WebServer.Application.Features.Products.Commands.CreateProduct;
using WebServer.Application.Features.Products.Queries.ProductPaging;

namespace WebServer.Api.Endpoints;

public class ProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("api/products");

        group.MapGet("/", ProductPaging);
        group.MapPost("/", CreateProduct);
    }

    public static async Task<IResult> ProductPaging(
        [AsParameters] ProductPagingQuery query,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            query,
            cancellationToken);

        return Results.Ok(result);
    }

    public static async Task<IResult> CreateProduct(
        CreateProductCommand command,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            command,
            cancellationToken);

        return Results.Created(
            $"/api/products/{result}",
            new
            {
                Id = result
            });
    }

}
