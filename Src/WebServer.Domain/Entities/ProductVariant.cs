using WebServer.Domain.Common;

namespace WebServer.Domain.Entities;

public class ProductVariant : Entity<Guid>
{
    public string SKU { get; private set; } = default!;

    public int StockQuantity { get; private set; }

    public string Size { get; private set; } = default!;

    public string Color { get; private set; } = default!;

    private ProductVariant()
    {
    }

    public ProductVariant(
        string sku,
        int stockQuantity,
        string size,
        string color)
    {
        Id = Guid.NewGuid();

        SKU = sku;
        StockQuantity = stockQuantity;
        Size = size;
        Color = color;
    }

    public void IncreaseStock(int quantity)
    {
        StockQuantity += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (StockQuantity < quantity)
            throw new Exception("Insufficient stock");

        StockQuantity -= quantity;
    }
}
