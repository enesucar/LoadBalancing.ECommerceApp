using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Trendyum.Blazor.Interfaces;
using Trendyum.Common.Models.Auth;
using Trendyum.Common.Models.Categories;
using Trendyum.Common.Models.Products;

namespace Trendyum.Blazor.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorageService;

    public BasketService(HttpClient httpClient, ILocalStorageService localStorageService)
    {
        _httpClient = httpClient;
        _localStorageService = localStorageService;
    }

    public async Task<List<ProductResponse>> ItemList()
    {
        var token = await _localStorageService.GetItemAsync<string>("token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await _httpClient.GetFromJsonAsync<List<ProductResponse>>("baskets");
    }

    public async Task AddItem(Guid id)
    {
        var token = await _localStorageService.GetItemAsync<string>("token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _httpClient.PostAsync($"baskets/add-item/{id}", null);
    }

    public async Task RemoveItem(Guid id)
    {
        var token = await _localStorageService.GetItemAsync<string>("token");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _httpClient.PostAsync($"baskets/remove-item/{id}", null);
    }
}
