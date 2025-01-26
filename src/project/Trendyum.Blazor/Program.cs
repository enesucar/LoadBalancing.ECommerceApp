using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Trendyum.Blazor;
using Trendyum.Blazor.Auth;
using Trendyum.Blazor.Interfaces;
using Trendyum.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBasketService, BasketService>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri("http://localhost:6001/api/v1/")
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddBlazoredToast();

await builder.Build().RunAsync();
