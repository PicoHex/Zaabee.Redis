namespace Aoxe.StackExchangeRedis.Abstractions;

public partial interface IAoxeRedisClient
{
    T? ListGetByIndex<T>(string key, long index);

    long ListInsertAfter<T>(string key, T? pivot, T? value);

    long ListInsertBefore<T>(string key, T? pivot, T? value);

    T? ListLeftPop<T>(string key);

    long ListLeftPush<T>(string key, T? value);

    long ListLeftPushRange<T>(string key, IEnumerable<T> values);

    long ListLength(string key);

    List<T?> ListRange<T>(string key, long start = 0, long stop = -1);

    long ListRemove<T>(string key, T? value, long count = 0);

    T? ListRightPop<T>(string key);

    long ListRightPush<T>(string key, T? value);

    long ListRightPushRange<T>(string key, IEnumerable<T> values);

    void ListSetByIndex<T>(string key, long index, T? value);

    void ListTrim(string key, long start, long stop);

    T? ListRightPopLeftPush<T>(string source, string destination);
}
