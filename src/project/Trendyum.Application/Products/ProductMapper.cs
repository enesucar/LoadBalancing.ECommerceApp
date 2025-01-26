using Shared.Guids;
using Trendyum.Application.Interfaces.Products;
using Trendyum.Common.Models.Products;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Products;

public class ProductMapper : IProductMapper
{
    private readonly IGuidGenerator _guidGenerator;

    public ProductMapper(IGuidGenerator guidGenerator)
    {
        _guidGenerator = guidGenerator;
    }

    public List<ProductResponse> MapToProductResponse(List<Product> products)
    {
        return products.Select(product => new ProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = product.Category.Name,
            ImageUrl = GetImageUrl(product.Image) ?? string.Empty
        }).ToList();
    }

    public ProductDetailResponse MapToProductDetailResponse(Product product)
    {
        return new ProductDetailResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            ImageUrl = GetImageUrl(product.Image) ?? string.Empty
        };
    }

    public Product MapToProduct(CreateProductRequest request)
    {
        return new Product()
        {
            Id = _guidGenerator.Generate(),
            Name = request.Name,
            Description = request.Description,
            CategoryId = request.CategoryId,
            Price = request.Price,
            Quantity = request.Quantity,
        };
    }

    public CreateProductResponse MapToCreateProductResponse(Product product)
    {
        return new CreateProductResponse()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Category = product.Category.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }

    private string? GetImageUrl(Resource? resource)
    {
        if (resource == null)
        {
            return string.Empty;
        }

        return $"https://trendyum.blob.core.windows.net/{resource.Path}/{resource.Name}";
    }
}

