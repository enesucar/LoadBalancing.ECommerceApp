namespace Shared.Caching;

public class CacheOptionsBuilder
{
    internal CacheOptions CacheOptions { get; set; } = new CacheOptions();

    public CacheOptionsBuilder()
    {
        CacheOptions = new CacheOptions();
    }

    public CacheOptionsBuilder SetCacheKeyPrefix(string cacheKeyPrefix)
    {
        CacheOptions.KeyPrefix = cacheKeyPrefix;
        return this;
    }

    internal CacheOptions Build()
    {
        return CacheOptions;
    }
}
