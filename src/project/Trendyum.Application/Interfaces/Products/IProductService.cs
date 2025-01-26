using Trendyum.Common.Models.Products;

namespace Trendyum.Application.Interfaces.Products;

public interface IProductService
{
    Task<List<ProductResponse>> GetListAsync(ProductListRequest request);

    Task<ProductDetailResponse> GetProductDetailAsync(Guid id);

    Task<CreateProductResponse> CreateAsync(CreateProductRequest request);

    Task DeleteAsync(Guid id);
}
