using Microsoft.AspNetCore.Components.Forms;
using Trendyum.Blazor.ViewModels.Products;
using Trendyum.Common.Models.Products;

namespace Trendyum.Blazor.Interfaces;

public interface IProductService
{
    Task<List<ProductResponse>> GetList(ProductListRequest? request = null);

    Task<ProductDetailResponse> GetDetail(Guid id);

    Task<CreateProductResponse> CreateAsync(CreateProductViewModel model, IBrowserFile file);
}
