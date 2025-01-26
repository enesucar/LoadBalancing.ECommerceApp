using Microsoft.EntityFrameworkCore;
using Shared.Guids;
using Shared.Security.Users;
using Shared.Utils.Exceptions;
using Trendyum.Application.Interfaces;
using Trendyum.Application.Interfaces.Baskets;
using Trendyum.Common.Models.Products;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Baskets;

public class BasketService : IBasketService
{
    private readonly ITrendyumDbContext _trendyumDbContext;
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentUser _currentUser;

    public BasketService(
        ITrendyumDbContext trendyumDbContext,
        IGuidGenerator guidGenerator,
        ICurrentUser currentUser)
    {
        _trendyumDbContext = trendyumDbContext;
        _guidGenerator = guidGenerator;
        _currentUser = currentUser;
    }

    public async Task<List<ProductResponse>> GetProductList()
    {
        var basket = await _trendyumDbContext.Baskets
            .Include(o => o.BasketItems)
                .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.Image)
            .Include(o => o.BasketItems)
                .ThenInclude(o => o.Product)
                    .ThenInclude(o => o.Category)
            .FirstOrDefaultAsync(o => o.UserId == _currentUser.UserId);

        if (basket == null)
        {
            return [];
        }

        return basket.BasketItems.Select(item => new ProductResponse()
        {
            Id = item.ProductId,
            Name = item.Product!.Name,
            Category = item.Product.Category.Name,
            Price = item.Product.Price,
            ImageUrl = GetImageUrl(item.Product.Image) ?? string.Empty
        }).ToList();
    }

    public async Task AddProductToBasket(Guid productId)
    {
        var product = await _trendyumDbContext.Products.FirstOrDefaultAsync(o => o.Id == productId);
        if (product == null)
        {
            throw new NotFoundException();
        }

        var basket = await _trendyumDbContext.Baskets
            .Include(o => o.BasketItems)
            .FirstOrDefaultAsync(o => o.UserId == _currentUser.UserId);
        if (basket == null)
        {
            var basketEntity = new Basket()
            {
                Id = productId,
                UserId = _currentUser.UserId!.Value
            };

            basket = (await _trendyumDbContext.Baskets.AddAsync(basketEntity)).Entity;
            await _trendyumDbContext.SaveChangesAsync();
        }

        if (basket.BasketItems.Any(o => o.ProductId == productId))
        {
            return;
        }

        var basketItem = new BasketItem()
        {
            BasketId = basket.Id,
            ProductId = productId,
            Product = product
        };


        basket.BasketItems.Add(basketItem);
        _trendyumDbContext.Baskets.Update(basket);

        await _trendyumDbContext.SaveChangesAsync();
    }

    public async Task RemoveProductFromBasket(Guid productId)
    {
        var product = await _trendyumDbContext.Products.FirstOrDefaultAsync(o => o.Id == productId);
        if (product == null)
        {
            throw new NotFoundException();
        }

        var basket = await _trendyumDbContext.Baskets
            .Include(o => o.BasketItems)
            .FirstOrDefaultAsync(o => o.UserId == _currentUser.UserId);
        if (basket == null)
        {
            return;
        }

        var basketItem = basket.BasketItems.FirstOrDefault(o => o.ProductId == productId);
        if (basketItem != null)
        {
            basket.BasketItems.Remove(basketItem);
            _trendyumDbContext.Baskets.Update(basket);
            await _trendyumDbContext.SaveChangesAsync();
        }
    }

    private string? GetImageUrl(Resource? resource)
    {
        if (resource == null)
        {
            return string.Empty;
        }

        return $"https://trendyum.blob.core.windows.net/{resource.Path}/{resource.Name}";
    }
}
