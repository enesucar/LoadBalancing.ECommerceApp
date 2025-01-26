using Trendyum.Common.Models.Products;

namespace Trendyum.Application.Interfaces.Baskets;

public interface IBasketService
{
    Task<List<ProductResponse>> GetProductList();

    Task AddProductToBasket(Guid productId);

    Task RemoveProductFromBasket(Guid productId);
}
