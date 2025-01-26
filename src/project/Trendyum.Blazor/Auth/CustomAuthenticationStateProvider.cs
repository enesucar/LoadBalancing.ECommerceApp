using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Trendyum.Blazor.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;
    private readonly AuthenticationState _anonymousAuthenticationState;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
        _anonymousAuthenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorageService.GetItemAsync<string>("token");
        if (string.IsNullOrWhiteSpace(token))
        {
            return _anonymousAuthenticationState;
        } 

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
        {
            jwt.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)!,
            jwt.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email)!,
            jwt.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Role)!,
        }, "auth"));

        return new AuthenticationState(claimsPrincipal);
    }

    public void NotifyUserLogin()
    {
        var authenticationState = GetAuthenticationStateAsync();
        NotifyAuthenticationStateChanged(authenticationState);
    }

    public void NotifyUserLogout()
    {
        var authenticationState = Task.FromResult(_anonymousAuthenticationState);
        NotifyAuthenticationStateChanged(authenticationState);
    }
}
