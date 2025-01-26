namespace Trendyum.Common.Models.Products;

public class ProductDetailResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public string ImageUrl { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public ProductDetailResponse()
    {
        Name = default!;
        Category = default!;
        ImageUrl = default!;
        Description = default!;
    }
}
