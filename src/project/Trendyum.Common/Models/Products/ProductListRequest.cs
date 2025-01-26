namespace Trendyum.Common.Models.Products;

public class ProductListRequest
{
    public List<Guid>? Categories { get; set; } = null;

    public string? SearchText { get; set; } = null;
}
