namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public async ValueTask<bool> AddAsync<T>(string key, T? entity, TimeSpan? expiry = null)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        expiry ??= _defaultExpiry;
        var bytes = _serializer.ToBytes(entity);
        return await _db.StringSetAsync(key, bytes, expiry);
    }

    public async ValueTask AddRangeAsync<T>(
        IDictionary<string, T?> entities,
        TimeSpan? expiry = null,
        bool isBatch = false
    )
    {
        if (entities == null || !entities.Any())
            return;
        expiry ??= _defaultExpiry;
        if (isBatch)
        {
            var batch = _db.CreateBatch();
            foreach (var kv in entities)
                await batch.StringSetAsync(kv.Key, _serializer.ToBytes(kv.Value), expiry);
            batch.Execute();
        }
        else
        {
            foreach (var kv in entities)
                await AddAsync(kv.Key, kv.Value, expiry);
        }
    }

    public async ValueTask<T?> GetAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return default;
        var value = await _db.StringGetAsync(key);
        return value.HasValue ? _serializer.FromBytes<T>(value) : default;
    }

    public async ValueTask<List<T>> GetAsync<T>(IEnumerable<string> keys, bool isBatch = false)
    {
        if (keys is null || !keys.Any())
            return new List<T>();
        List<T> result;
        if (isBatch)
        {
            var values = await _db.StringGetAsync(keys.Select(p => (RedisKey)p).ToArray());
            result = values.Select(value => _serializer.FromBytes<T>(value)).ToList();
        }
        else
        {
            result = new List<T>();
            foreach (var key in keys)
                result.Add(await GetAsync<T>(key));
        }

        return result;
    }

    public async ValueTask<bool> AddAsync(string key, long value, TimeSpan? expiry = null)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        expiry ??= _defaultExpiry;
        return await _db.StringSetAsync(key, value, expiry);
    }

    public async ValueTask<bool> AddAsync(string key, double value, TimeSpan? expiry = null)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
        expiry ??= _defaultExpiry;
        return await _db.StringSetAsync(key, value, expiry);
    }

    public async ValueTask<double> IncrementAsync(string key, double value) =>
        await _db.StringIncrementAsync(key, value);

    public async ValueTask<long> IncrementAsync(string key, long value) =>
        await _db.StringIncrementAsync(key, value);
}
