using Microsoft.EntityFrameworkCore;
using Shared.Caching;
using Shared.Utils.Exceptions;
using Trendyum.Application.Interfaces;
using Trendyum.Application.Interfaces.Products;
using Trendyum.Application.Interfaces.Resources;
using Trendyum.Common.Models.Products;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Products;

public class ProductService : IProductService
{
    private readonly ITrendyumDbContext _trendyumDbContext;
    private readonly IProductMapper _productMapper;
    private readonly ICacheService _cacheService;
    private readonly IResourceService _resourceService;
    private readonly ICacheKeyGenerator _cacheKeyGenerator;

    public ProductService(
        ITrendyumDbContext trendyumDbContext,
        IProductMapper productMapper,
        ICacheService cacheService,
        IResourceService resourceService,
        ICacheKeyGenerator cacheKeyGenerator)
    {
        _trendyumDbContext = trendyumDbContext;
        _productMapper = productMapper;
        _cacheService = cacheService;
        _resourceService = resourceService;
        _cacheKeyGenerator = cacheKeyGenerator;
    }

    public async Task<List<ProductResponse>> GetListAsync(ProductListRequest request)
    {
        var query = _trendyumDbContext.Products.AsQueryable();
        query = (!string.IsNullOrEmpty(request.SearchText))
                    ? query.Where(o => o.Name.Contains(request.SearchText)) : query;
        query = (request.Categories != null && request.Categories.Count > 0) 
                    ? query.Where(o => request.Categories.Contains(o.CategoryId)) : query;

        var products = await query.Include(o => o.Image).Include(o => o.Category).ToListAsync();
        return _productMapper.MapToProductResponse(products);
    }

    public async Task<ProductDetailResponse> GetProductDetailAsync(Guid id)
    {
        return (await _cacheService.GetOrSetAsync(
           _cacheKeyGenerator.Generate($"{ProductConstants.CacheKeyPrefix}.Detail", id),
           () => GetProductDetailFromDatabaseAsync(id),
           TimeSpan.FromHours(1)))!;
    }

    public async Task<CreateProductResponse> CreateAsync(CreateProductRequest request)
    {
        var category = await _trendyumDbContext.Categories.FirstOrDefaultAsync(o => o.Id == request.CategoryId);
        if (category == null)
        {
            throw new NotFoundException();
        }

        var image = await _resourceService.CreateAsync(request.File);
        var product = _productMapper.MapToProduct(request);

        product.Category = category;
        product.Image = image;
        product.CreationDate = DateTime.Now;

        await _trendyumDbContext.Products.AddAsync(product);
        await _trendyumDbContext.SaveChangesAsync();

        return _productMapper.MapToCreateProductResponse(product);
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await GetProductAsync(id);
        _trendyumDbContext.Products.Remove(product);
        await _trendyumDbContext.SaveChangesAsync();
        await _resourceService.DeleteAsync(product.Id);
    }

    private async Task<ProductDetailResponse> GetProductDetailFromDatabaseAsync(Guid id)
    {
        var product = await GetProductAsync(id, true);
        return _productMapper.MapToProductDetailResponse(product);
    }

    private async Task<Product> GetProductAsync(Guid id, bool includeDetails = false)
    {
        var query = _trendyumDbContext.Products.AsQueryable();
        query = includeDetails ? query.Include(o => o.Category).Include(o => o.Image) : query;
        var product = await query.FirstOrDefaultAsync(o => o.Id == id);

        if (product == null)
        {
            throw new NotFoundException();
        }

        return product;
    }
}
