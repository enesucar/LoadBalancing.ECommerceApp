using System.Net.Http.Json;
using Trendyum.Blazor.Interfaces;
using Trendyum.Common.Models.Auth;
using Trendyum.Common.Models.Products;

namespace Trendyum.Blazor.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserLoginResponse> LoginAsync(UserLoginRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("auth/login", request);
        return await result.Content.ReadFromJsonAsync<UserLoginResponse>();
    }

    public async Task<UserRegisterResponse> RegisterAsync(UserRegisterRequest request)
    {
        var result = await _httpClient.PostAsJsonAsync("auth/register", request);
        return await result.Content.ReadFromJsonAsync<UserRegisterResponse>();
    }
}
