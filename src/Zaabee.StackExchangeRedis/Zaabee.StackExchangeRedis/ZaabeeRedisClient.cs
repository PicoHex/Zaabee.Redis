﻿using System;
using StackExchange.Redis;
using Zaabee.StackExchangeRedis.Abstractions;
using Zaabee.StackExchangeRedis.ISerialize;

namespace Zaabee.StackExchangeRedis
{
    public partial class ZaabeeRedisClient : IZaabeeRedisClient, IDisposable
    {
        private IConnectionMultiplexer _conn;
        private IDatabase _db;
        private ISerializer _serializer;
        private TimeSpan _defaultExpiry;

        public ZaabeeRedisClient(RedisConfig config, ISerializer serializer) =>
            Init(config.Options, config.DefaultExpiry, serializer);

        public ZaabeeRedisClient(string connectionString, TimeSpan defaultExpiry, ISerializer serializer) =>
            Init(ConfigurationOptions.Parse(connectionString), defaultExpiry, serializer);

        public ZaabeeRedisClient(ConfigurationOptions options, TimeSpan defaultExpiry, ISerializer serializer) =>
            Init(options, defaultExpiry, serializer);

        private void Init(ConfigurationOptions options, TimeSpan defaultExpiry, ISerializer serializer)
        {
            _defaultExpiry = defaultExpiry;
            _conn = ConnectionMultiplexer.Connect(options);
            _serializer = serializer;
            _db = _conn.GetDatabase();

            // var endPoints = _conn.GetEndPoints();
            // var ii = _conn.GetServer(endPoints[0]);
            // var i0 = _conn.GetCounters();
            // var i2 = _conn.GetSubscriber();
            // var i1 = _conn.GetStatus();
            // var i3 = _conn.GetHashSlot();
            // var i4 = _conn.GetStormLog();
        }

        public void Dispose() => _conn.Dispose();
    }
}