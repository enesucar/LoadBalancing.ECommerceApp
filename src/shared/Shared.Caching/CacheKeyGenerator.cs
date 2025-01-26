using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;

namespace Shared.Caching;

public class CacheKeyGenerator : ICacheKeyGenerator
{
    private readonly CacheOptions _cacheOptions;

    public CacheKeyGenerator(IOptions<CacheOptions> cacheOptions)
    {
        _cacheOptions = cacheOptions.Value;
    }

    public string Generate(string name, params object[] values)
    {
        string valuesHash = string.Empty;

        if (values.Length > 0)
        {
            string valuesPart = string.Join(":", values.Select(value => value.ToString() ?? string.Empty));
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(valuesPart));
            valuesHash = Convert.ToBase64String(hashBytes);
        }

        return $"{GetCacheKeyPrefix()}{name}({valuesHash})";
    }

    private string GetCacheKeyPrefix()
    {
        return _cacheOptions.KeyPrefix.IsNullOrWhiteSpace() ? string.Empty : _cacheOptions.KeyPrefix.AppendToEnd(".");
    }
}

