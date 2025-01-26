using Trendyum.Common.Models.Products;
using Trendyum.Domain.Entities;

namespace Trendyum.Application.Interfaces.Products;

public interface IProductMapper
{
    List<ProductResponse> MapToProductResponse(List<Product> products);

    ProductDetailResponse MapToProductDetailResponse(Product product);

    Product MapToProduct(CreateProductRequest request);

    CreateProductResponse MapToCreateProductResponse(Product product);
}
