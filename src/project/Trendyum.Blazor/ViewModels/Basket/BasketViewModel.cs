namespace Trendyum.Blazor.ViewModels.Basket;

public class BasketViewModel
{
    public List<BasketItemViewModel>? BasketItems { get; set; }
}

public class BasketItemViewModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = default!;
}

