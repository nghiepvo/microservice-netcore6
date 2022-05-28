using Redis.OM;
using Redis.OM.Contracts;

namespace Basket.API.Infrastructures.Redis;

internal static class CacheSettings
{
    public const string ConnectionString = $"{nameof(CacheSettings)}:{nameof(ConnectionString)}";
}

public static class CACHE
{
    public static RedisConnectionProvider Provider { get; private set; } = default!;
    public static IRedisConnection Context  => Provider.Connection;
    public static void UseRedis(this WebApplication app)
    {
        var connectionString = app.Configuration.GetValue<string>(CacheSettings.ConnectionString);
        Provider = new RedisConnectionProvider(connectionString);
    }
}