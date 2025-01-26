namespace Trendyum.Common.Models.Categories;

public class CreateCategoryResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public CreateCategoryResponse()
    {
        Name = default!;
    }
}
