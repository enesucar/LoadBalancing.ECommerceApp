namespace Trendyum.Common.Models.Categories;

public class CategoryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public CategoryResponse()
    {
        Name = default!;
    }
}
