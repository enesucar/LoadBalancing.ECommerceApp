using Microsoft.EntityFrameworkCore;
using Shared.Caching;
using Shared.Utils.Exceptions;
using Trendyum.Application.Interfaces;
using Trendyum.Application.Interfaces.Categories;
using Trendyum.Common.Models.Categories;

namespace Trendyum.Application.Categories;

public class CategoryService : ICategoryService
{
    private readonly ITrendyumDbContext _trendyumDbContext;
    private readonly ICategoryMapper _categoryMapper;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyGenerator _cacheKeyGenerator;

    public CategoryService(
        ITrendyumDbContext trendyumDbContext,
        ICategoryMapper categoryMapper,
        ICacheService cacheService,
        ICacheKeyGenerator cacheKeyGenerator)
    {
        _trendyumDbContext = trendyumDbContext;
        _categoryMapper = categoryMapper;
        _cacheService = cacheService;
        _cacheKeyGenerator = cacheKeyGenerator;
    }

    public async Task<List<CategoryResponse>> GetListAsync()
    {
        return (await _cacheService.GetOrSetAsync(
            _cacheKeyGenerator.Generate($"{CategoryConstants.CacheKeyPrefix}.List"),
            GetListFromDatabaseAsync,
            TimeSpan.FromHours(6)))!;
    }

    public async Task<CreateCategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var category = _categoryMapper.MapToCategory(request);

        await _trendyumDbContext.Categories.AddAsync(category);
        await _trendyumDbContext.SaveChangesAsync();

        await _cacheService.RemoveByPatternAsync(CategoryConstants.CacheKeyPrefix);

        return _categoryMapper.MapToCreateCategoryResponse(category);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = _trendyumDbContext.Categories.FirstOrDefault(o => o.Id == id);
        if (category == null)
        {
            throw new NotFoundException();
        }

        _trendyumDbContext.Categories.Remove(category);
        await _trendyumDbContext.SaveChangesAsync();

        await _cacheService.RemoveByPatternAsync(CategoryConstants.CacheKeyPrefix);
    }

    private async Task<List<CategoryResponse>> GetListFromDatabaseAsync()
    {
        var categories = await _trendyumDbContext.Categories.ToListAsync();
        return _categoryMapper.MapToCategoryResponse(categories);
    }
}
