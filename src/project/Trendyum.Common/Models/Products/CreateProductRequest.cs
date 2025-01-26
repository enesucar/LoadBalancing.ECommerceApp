using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Trendyum.Common.Models.Products;

public class CreateProductRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public IFormFile File { get; set; }

    public CreateProductRequest()
    {
        Name = default!;
        Description = default!;
        File = default!;
    }
}
