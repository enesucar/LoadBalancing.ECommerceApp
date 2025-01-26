using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Shared.Json;

namespace Shared.Caching.Redis;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _database;
    private readonly IServer _server;
    private readonly IJsonSerializer _jsonSerilazer;

    public RedisCacheService(
        IConnectionMultiplexer connectionMultiplexer,
        IJsonSerializer jsonSerilazer,
        IOptions<RedisCacheOptions> redisCacheOptions)
    {
        var endpoints = connectionMultiplexer.GetEndPoints();
        _server = connectionMultiplexer.GetServer(endpoints.First());
        _database = connectionMultiplexer.GetDatabase(redisCacheOptions.Value.Database);
        _jsonSerilazer = jsonSerilazer;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var cachedData = await _database.StringGetAsync(key);
        return cachedData.IsNullOrEmpty ? default : _jsonSerilazer.Deserialize<T>(cachedData!);
    }

    public async Task SetAsync(string key, object data)
    {
        var jsonData = _jsonSerilazer.Serialize(data);
        await _database.StringSetAsync(key, jsonData);
    }

    public async Task SetAsync(string key, object data, TimeSpan expiration)
    {
        var jsonData = _jsonSerilazer.Serialize(data);
        await _database.StringSetAsync(key, jsonData);
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> data)
    {
        var cacheValue = await GetAsync<T>(key);
        if (cacheValue == null)
        {
            var value = await data.Invoke();
            if (value == null)
            {
                return default;
            }

            await SetAsync(key, value);
            cacheValue = value;
        }
        return cacheValue;
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> data, TimeSpan expiration)
    {
        var cacheValue = await GetAsync<T>(key);
        if (cacheValue == null)
        {
            var value = await data.Invoke();
            if (value == null)
            {
                return default;
            }

            await SetAsync(key, value, expiration);
            cacheValue = value;
        }
        return cacheValue;
    }

    public async Task<bool> AnyAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task RemoveByPatternAsync(string pattern)
    {
        var keys = _server.Keys(pattern: $"*{pattern}*");
        foreach (var key in keys)
        {
            await _database.KeyDeleteAsync(key);
        }
    }

    public async Task ClearAsync()
    {
        await _server.FlushDatabaseAsync(_database.Database);
    }
}
