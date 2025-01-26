using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.Caching;
using Shared.Json;
using Trendyum.Application.Interfaces;
using Trendyum.Domain.Entities;
using Trendyum.Infrastructure.Data.Contexts;
using Trendyum.Infrastructure.Storage;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddSequentialGuidGenerator();

        services.AddJsonSerializer(options =>
        {
            options.UseNewtonsoft(services, newtonsoftOptions => { });
        });

        services.AddDbContext<TrendyumDbContext>(options => { options.UseSqlServer(connectionString); });
        services.AddScoped<ITrendyumDbContext, TrendyumDbContext>();
        services.AddScoped<TrendyumDbContextInitializer>();

        services.AddCache(options =>
        {
            options.SetCacheKeyPrefix("Trendyum");
            options.UseRedis(services, redisOptions =>
            {
                redisOptions.Host = "127.0.0.1";
                redisOptions.Port = 6379;
                redisOptions.Database = 0;
            });
        });

        services.AddDefaultIdentity<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        }).AddDefaultTokenProviders()
          .AddRoles<Role>()
          .AddEntityFrameworkStores<TrendyumDbContext>();

        services.AddSecurity(options =>
        {
            options.Issuer = configuration.GetValue<string>("AccessTokenOptions:Issuer")!;
            options.Audience = configuration.GetValue<string>("AccessTokenOptions:Audience")!;
            options.Expiration = configuration.GetValue<int?>("AccessTokenOptions:Expiration") ?? 360;
            options.SecurityKey = configuration.GetValue<string>("AccessTokenOptions:SecurityKey")!;
        });

        services.AddScoped<IStorage, AzureStorage>();

        return services;
    }
}

