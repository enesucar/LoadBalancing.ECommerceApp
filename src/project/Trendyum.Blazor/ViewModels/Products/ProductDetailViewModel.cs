namespace Trendyum.Blazor.ViewModels.Products;

public class ProductDetailViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public string Category { get; set; } = default!;

    public string ImageUrl { get; set; } = default!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
