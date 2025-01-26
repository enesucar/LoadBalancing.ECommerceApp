    using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Web;
using Microsoft.AspNetCore.Components.Forms;
using Trendyum.Blazor.Interfaces;
using Trendyum.Blazor.ViewModels.Products;
using Trendyum.Common.Models.Products;

namespace Trendyum.Blazor.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
     
    public async Task<List<ProductResponse>> GetList(ProductListRequest? request = null)
    {
        var queryParams = HttpUtility.ParseQueryString(string.Empty);
       
        if (request?.Categories != null && request.Categories.Any())
        {
            for (int i = 0; i < request.Categories.Count; i++)
            {
                queryParams[$"Categories[{i}]"] = request.Categories[i].ToString();
            }
        }
        
        if (!string.IsNullOrEmpty(request?.SearchText))
        {
            queryParams["SearchText"] = request.SearchText;
        }

        var url = string.IsNullOrEmpty(queryParams.ToString()) ? "products" : $"products?{queryParams.ToString()}";
        return await _httpClient.GetFromJsonAsync<List<ProductResponse>>(url);
    }

    public async Task<ProductDetailResponse> GetDetail(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<ProductDetailResponse>($"products/{id}");
    }

    public async Task<CreateProductResponse> CreateAsync(CreateProductViewModel model, IBrowserFile file)
    {
        using var content = new MultipartFormDataContent();

        var fileStream = file.OpenReadStream(); // Open file stream
        var streamContent = new StreamContent(fileStream);
        streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        HttpContent name = new StringContent(model.Name);
        HttpContent description = new StringContent(model.Description);
        HttpContent categoryId = new StringContent(model.CategoryId.ToString());
        HttpContent price = new StringContent(model.Price.ToString());
        HttpContent quantity = new StringContent(model.Quantity.ToString());

 
        content.Add(streamContent, "\"file\"", file.Name);
        content.Add(name, "\"name\"");
        content.Add(description, "\"description\"");
        content.Add(categoryId, "\"categoryId\"");
        content.Add(price, "\"price\"");
        content.Add(quantity, "\"quantity\"");

        var result = await _httpClient.PostAsync("products", content);
        var a = await result.Content.ReadAsStringAsync();
        return await result.Content.ReadFromJsonAsync<CreateProductResponse>();
    } 
}
