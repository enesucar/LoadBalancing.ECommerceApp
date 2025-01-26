using System.Net.Http.Json;
using Trendyum.Blazor.Interfaces;
using Trendyum.Common.Models.Categories;

namespace Trendyum.Blazor.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<CategoryResponse>> GetListAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategoryResponse>>("categories");
    }
}
