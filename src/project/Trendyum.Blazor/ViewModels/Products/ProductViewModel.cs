namespace Trendyum.Blazor.ViewModels.Products;

public class ProductViewModel
{
    public List<ProductCategoryViewModel>? Categories { get; set; }

    public List<ProductItemViewModel>? Products { get; set; }

    public string SearchText { get; set; } = string.Empty;
}

public class ProductItemViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = default!;
}

public class ProductCategoryViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public bool Checked { get; set; }
}