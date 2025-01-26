using Trendyum.Common.Models.Categories;

namespace Trendyum.Blazor.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetListAsync();
}
