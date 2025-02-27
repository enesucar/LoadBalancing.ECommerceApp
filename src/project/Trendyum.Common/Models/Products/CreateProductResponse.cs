﻿namespace Trendyum.Common.Models.Products;

public class CreateProductResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public CreateProductResponse()
    {
        Name = default!;
        Description = default!;
        Category = default!;
    }
}
