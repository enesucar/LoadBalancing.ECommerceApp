using Microsoft.AspNetCore.Identity;
using Trendyum.Application.Auth;
using Trendyum.Application.Baskets;
using Trendyum.Application.Categories;
using Trendyum.Application.Interfaces.Auth;
using Trendyum.Application.Interfaces.Baskets;
using Trendyum.Application.Interfaces.Categories;
using Trendyum.Application.Interfaces.Products;
using Trendyum.Application.Interfaces.Resources;
using Trendyum.Application.Products;
using Trendyum.Application.Resources;
using Trendyum.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IBasketService, BasketService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddSingleton<IProductMapper, ProductMapper>();
        services.AddSingleton<ICategoryMapper, CategoryMapper>();

        services.AddScoped<IUserValidator<User>, CustomUserValidator>();

        return services;
    }
}
