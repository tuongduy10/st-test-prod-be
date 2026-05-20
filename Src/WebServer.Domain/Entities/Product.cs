using WebServer.Domain.Common;
using WebServer.Domain.Common.Enums;

namespace WebServer.Domain.Entities;

[Table("Products")]
public class Product : AuditableEntity<Guid>
{
    private readonly List<ProductVariant> _variants = [];

    public string Name { get; private set; } = default!;

    public string Slug { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public decimal Price { get; private set; }

    public ProductStatus Status { get; private set; }

    public IReadOnlyCollection<ProductVariant> Variants
        => _variants.AsReadOnly();

    private Product()
    {
    }

    public Product(
        string name,
        string slug,
        string description,
        decimal price)
    {
        Id = Guid.NewGuid();

        Name = name;
        Slug = slug;
        Description = description;
        Price = price;

        Status = ProductStatus.Draft;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(
        string name,
        string description,
        decimal price)
    {
        Name = name;
        Description = description;
        Price = price;

        UpdatedAt = DateTime.UtcNow;
    }

    public void Publish()
    {
        Status = ProductStatus.Published;
    }

    public void AddVariant(ProductVariant variant)
    {
        _variants.Add(variant);
    }

    public void SoftDelete()
    {
        IsDeleted = true;
    }
}