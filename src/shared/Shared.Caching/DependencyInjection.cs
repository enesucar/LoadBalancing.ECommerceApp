using Shared.Caching;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddCache(
        this IServiceCollection services,
        Action<CacheOptionsBuilder> configureCacheOptionsBuilder)
    {
        var cacheOptionsBuilder = new CacheOptionsBuilder();
        configureCacheOptionsBuilder(cacheOptionsBuilder);

        var cacheOptions = cacheOptionsBuilder.Build();

        return services
            .AddSingleton<ICacheKeyGenerator, CacheKeyGenerator>()
            .Configure<CacheOptions>(o => { o.KeyPrefix = cacheOptions.KeyPrefix; });
    }
}
