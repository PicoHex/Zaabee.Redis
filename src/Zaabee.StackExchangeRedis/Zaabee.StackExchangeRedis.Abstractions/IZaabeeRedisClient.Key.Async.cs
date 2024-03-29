namespace Zaabee.StackExchangeRedis.Abstractions;

public partial interface IZaabeeRedisClient
{
    ValueTask<bool> DeleteAsync(string key);

    ValueTask<long> DeleteAllAsync(IEnumerable<string> keys);

    ValueTask<bool> ExistsAsync(string key);

    ValueTask<bool> ExpireAsync(string key, TimeSpan? timeSpan);
}
