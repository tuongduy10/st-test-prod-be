using WebServer.Application.Features.Products.Commands.CreateProduct;
using WebServer.Application.Features.Products.Commands.IncreaseProductQuantity;
using WebServer.Application.Features.Products.Commands.SoftDeleteProduct;
using WebServer.Application.Features.Products.Commands.UpdateProduct;
using WebServer.Application.Features.Products.Queries.ProductPaging;

namespace WebServer.Api.Endpoints;

public class ProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder route)
    {
        var group = route.MapGroup("api/products");

        group.MapGet("/", ProductPaging);
        group.MapPost("/", CreateProduct);
        group.MapPut("/{id:guid}", UpdateProduct);
        group.MapDelete("/{id:guid}", SoftDeleteProduct);
        group.MapPatch(
            "/{productId:guid}/variants/{variantId:guid}/increase-quantity",
            IncreaseQuantity);
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

        return Results.Created();
    }

    public static async Task<IResult> UpdateProduct(
        Guid id,
        UpdateProductCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(
            new UpdateProductCommand(
                id,
                request.Name,
                request.Description,
                request.Price),
            cancellationToken);

        return Results.Ok();
    }

    public static async Task<IResult> SoftDeleteProduct(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(
            new SoftDeleteProductCommand(id),
            cancellationToken);

        return Results.Ok();
    }

    public static async Task<IResult> IncreaseQuantity(
        IncreaseProductQuantityCommand request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(
            new IncreaseProductQuantityCommand(
                request.ProductId,
                request.VariantId,
                request.Quantity),
            cancellationToken);

        return Results.NoContent();
    }
}
