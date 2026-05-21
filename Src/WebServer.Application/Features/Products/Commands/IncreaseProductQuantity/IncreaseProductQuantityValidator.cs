namespace WebServer.Application.Features.Products.Commands.IncreaseProductQuantity;

public class IncreaseProductQuantityValidator
    : AbstractValidator<IncreaseProductQuantityCommand>
{
    public IncreaseProductQuantityValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.VariantId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}
