using WebServer.Application.Common.Models.Responses;
using WebServer.Application.Features.Products.Dtos;

namespace WebServer.Application.Features.Products.Queries.ProductPaging;

public record ProductPagingQuery(
    int Page = 1,
    int PageSize = 10)
    : IRequest<PagedResult<ProductResponseDto>>;
