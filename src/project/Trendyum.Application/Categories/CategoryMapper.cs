using Shared.Guids;
using Trendyum.Application.Interfaces.Categories;
using Trendyum.Common.Models.Categories;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Categories;

public class CategoryMapper : ICategoryMapper
{
    private readonly IGuidGenerator _guidGenerator;

    public CategoryMapper(IGuidGenerator guidGenerator)
    {
        _guidGenerator = guidGenerator;
    }

    public List<CategoryResponse> MapToCategoryResponse(List<Category> categories)
    {
        return categories.Select(category => new CategoryResponse()
        {
            Id = category.Id,
            Name = category.Name
        }).ToList();
    }

    public Category MapToCategory(CreateCategoryRequest request)
    {
        return new Category()
        {
            Id = _guidGenerator.Generate(),
            Name = request.Name
        };
    }

    public CreateCategoryResponse MapToCreateCategoryResponse(Category category)
    {
        return new CreateCategoryResponse()
        {
            Id = category.Id,
            Name = category.Name
        };
    }
}
