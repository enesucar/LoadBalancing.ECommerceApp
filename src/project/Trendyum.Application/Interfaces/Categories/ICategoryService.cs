using Trendyum.Common.Models.Categories;

namespace Trendyum.Application.Interfaces.Categories;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetListAsync();

    Task<CreateCategoryResponse> CreateAsync(CreateCategoryRequest request);

    Task DeleteAsync(Guid id);
}
