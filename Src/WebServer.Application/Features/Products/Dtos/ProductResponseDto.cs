using WebServer.Domain.Common.Enums;

namespace WebServer.Application.Features.Products.Dtos;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public ProductStatus Status { get; set; }
}
