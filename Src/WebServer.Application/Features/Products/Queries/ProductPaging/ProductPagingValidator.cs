using WebServer.Application.Common.Models.Responses;

namespace WebServer.Application.Features.Products.Queries.ProductPaging;

public class ProductPagingValidator
    : AbstractValidator<ProductPagingQuery>
{
    public ProductPagingValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(
                PagedResult<object>.UpperPageSize);
    }
}
