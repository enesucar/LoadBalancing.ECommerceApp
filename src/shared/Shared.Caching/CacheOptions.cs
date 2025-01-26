namespace Shared.Caching;

public class CacheOptions
{
    public string KeyPrefix { get; set; }

    public CacheOptions()
    {
        KeyPrefix = string.Empty;
    }
}
