namespace Zaabee.StackExchangeRedis;

public partial class ZaabeeRedisDatabase
{
    public T? ListGetByIndex<T>(string key, long index) =>
        _serializer.FromBytes<T>(_db.ListGetByIndex(key, index));

    public long ListInsertAfter<T>(string key, T? pivot, T? value) =>
        _db.ListInsertAfter(key, _serializer.ToBytes(pivot), _serializer.ToBytes(value));

    public long ListInsertBefore<T>(string key, T? pivot, T? value) =>
        _db.ListInsertBefore(key, _serializer.ToBytes(pivot), _serializer.ToBytes(value));

    public T? ListLeftPop<T>(string key) => _serializer.FromBytes<T>(_db.ListLeftPop(key));

    public long ListLeftPush<T>(string key, T? value) =>
        _db.ListLeftPush(key, (RedisValue)_serializer.ToBytes(value));

    public long ListLeftPushRange<T>(string key, IEnumerable<T> values) => _db.ListLeftPush(key,
        values.Select(value => (RedisValue)_serializer.ToBytes(value)).ToArray());

    public long ListLength(string key) => _db.ListLength(key);

    public IList<T> ListRange<T>(string key, long start = 0, long stop = -1) => _db.ListRange(key, start, stop)
        .Select(value => _serializer.FromBytes<T>(value)).ToList();

    public long ListRemove<T>(string key, T? value, long count = 0) =>
        _db.ListRemove(key, _serializer.ToBytes(value), count);

    public T? ListRightPop<T>(string key) => _serializer.FromBytes<T>(_db.ListRightPop(key));

    public T? ListRightPopLeftPush<T>(string source, string destination) =>
        _serializer.FromBytes<T>(_db.ListRightPopLeftPush(source, destination));

    public long ListRightPush<T>(string key, T? value) => _db.ListRightPush(key, _serializer.ToBytes(value));

    public long ListRightPushRange<T>(string key, IEnumerable<T> values) => _db.ListRightPush(key,
        values.Select(value => (RedisValue)_serializer.ToBytes(value)).ToArray());

    public void ListSetByIndex<T>(string key, long index, T? value) =>
        _db.ListSetByIndex(key, index, _serializer.ToBytes(value));

    public void ListTrim(string key, long start, long stop) => _db.ListTrim(key, start, stop);
}