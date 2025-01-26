namespace Trendyum.Common.Models.Categories;

public class CreateCategoryRequest
{
    public string Name { get; set; }

    public CreateCategoryRequest()
    {
        Name = default!;
    }
}
