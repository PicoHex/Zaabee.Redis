namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisClient
{
    public async ValueTask<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null)
    {
        var bytes = serializer.ToBytes(entity);
        return await db.StringSetAsync(key, bytes, expiry ?? defaultExpiry);
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        var value = await db.StringGetAsync(key);
        return value.HasValue ? serializer.FromBytes<T>(value) : default;
    }

    public async ValueTask<List<T?>> GetAsync<T>(IEnumerable<string> keys)
    {
        var values = await db.StringGetAsync(keys.Select(p => (RedisKey)p).ToArray());
        return values.Select(value => serializer.FromBytes<T>(value)).ToList();
    }

    public async Task<bool> AddAsync(string key, long value, TimeSpan? expiry = null) =>
        await db.StringSetAsync(key, value, expiry ?? defaultExpiry);

    public async Task<bool> AddAsync(string key, double value, TimeSpan? expiry = null) =>
        await db.StringSetAsync(key, value, expiry ?? defaultExpiry);

    public async Task<double> IncrementAsync(string key, double value) =>
        await db.StringIncrementAsync(key, value);

    public async Task<long> IncrementAsync(string key, long value) =>
        await db.StringIncrementAsync(key, value);
}
