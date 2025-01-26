using Trendyum.Common.Models.Products;

namespace Trendyum.Blazor.Interfaces;

public interface IBasketService
{
    Task<List<ProductResponse>> ItemList();

    Task AddItem(Guid id);

    Task RemoveItem(Guid id);
}
