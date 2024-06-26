namespace Aoxe.StackExchangeRedis.Abstractions;

public partial interface IAoxeRedisClient
{
    bool HashAdd<T>(string key, string entityKey, T? entity);

    void HashAddRange<T>(string key, IDictionary<string, T?> entities);

    bool HashDelete(string key, string entityKey);

    long HashDeleteRange(string key, IEnumerable<string> entityKeys);

    T? HashGet<T>(string key, string entityKey);

    Dictionary<string, T?> HashGet<T>(string key);

    List<T?> HashGetRange<T>(string key, IEnumerable<string> entityKeys);

    List<string> HashGetAllEntityKeys(string key);

    long HashCount(string key);
}
