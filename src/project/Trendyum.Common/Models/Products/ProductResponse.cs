namespace Trendyum.Common.Models.Products;

public class ProductResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    public string Category { get; set; }

    public ProductResponse()
    {
        Name = default!;
        ImageUrl = default!;
    }
}
