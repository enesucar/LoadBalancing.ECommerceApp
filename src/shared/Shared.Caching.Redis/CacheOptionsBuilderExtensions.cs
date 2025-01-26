using Microsoft.Extensions.DependencyInjection;
using Shared.Caching.Redis;
using StackExchange.Redis;

namespace Shared.Caching;

public static class CacheOptionsBuilderExtensions
{
    public static void UseRedis(
        this CacheOptionsBuilder cacheOptionsBuilder,
        IServiceCollection services,
        Action<RedisCacheOptions> configureRedisCacheOptions)
    {
        RedisCacheOptions redisCacheOptions = new RedisCacheOptions();
        configureRedisCacheOptions(redisCacheOptions);

        var endPoints = new EndPointCollection
        {
            { redisCacheOptions.Host, redisCacheOptions.Port },
        };

        services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(new ConfigurationOptions()
        {
            EndPoints = endPoints
        }));

        services
            .Configure(configureRedisCacheOptions)
            .AddSingleton<ICacheService, RedisCacheService>();
    }
}
