namespace Shared.Caching.Redis;

public class RedisCacheOptions
{
    public string Host { get; set; }

    public int Port { get; set; }

    public int Database { get; set; }

    public RedisCacheOptions()
    {
        Host = default!;
        Database = 0;
    }
}
