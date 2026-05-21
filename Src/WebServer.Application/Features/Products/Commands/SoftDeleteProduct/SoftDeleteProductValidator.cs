namespace WebServer.Application.Features.Products.Commands.SoftDeleteProduct;

public class SoftDeleteProductValidator
    : AbstractValidator<SoftDeleteProductCommand>
{
    public SoftDeleteProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
