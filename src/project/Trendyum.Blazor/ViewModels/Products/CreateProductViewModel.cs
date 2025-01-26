namespace Trendyum.Blazor.ViewModels.Products;

public class CreateProductViewModel
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
