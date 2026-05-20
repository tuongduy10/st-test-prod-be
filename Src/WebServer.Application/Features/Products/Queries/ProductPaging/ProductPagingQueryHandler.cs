using WebServer.Application.Common.Models.Responses;
using WebServer.Application.Features.Products.Dtos;
using WebServer.Domain.Common.Repositories;

namespace WebServer.Application.Features.Products.Queries.ProductPaging;

public class ProductPagingQueryHandler
    : IRequestHandler<ProductPagingQuery, PagedResult<ProductResponseDto>>
{
    private readonly IProductReadRepository _repository;

    public ProductPagingQueryHandler(
        IProductReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<ProductResponseDto>> Handle(
        ProductPagingQuery request,
        CancellationToken cancellationToken)
    {
        var query = _repository
            .GetQueryable()
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ProductResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        var pagedResults = await PagedResult<ProductResponseDto>
            .CreateAsync(query, request.Page, request.PageSize);
        return pagedResults;
    }
}
