using Trendyum.Common.Models.Categories;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Interfaces.Categories;

public interface ICategoryMapper
{
    List<CategoryResponse> MapToCategoryResponse(List<Category> categories);

    Category MapToCategory(CreateCategoryRequest request);

    CreateCategoryResponse MapToCreateCategoryResponse(Category category);
}
