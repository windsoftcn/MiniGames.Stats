using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace MiniGames.Stats.Providers
{
    public interface IRedisProvider
    {
        IDatabase Database { get; }

        Task<RedisValue> StringGetAsync(RedisKey key);

        Task<long> StringIncrementAsync(RedisKey key);

        Task<bool> StringSetAsync(RedisKey key, RedisValue value);

        Task<bool> HashSetAsync(RedisKey key, RedisValue field, RedisValue value);

        Task<RedisValue> HashGetAsync(RedisKey key, RedisValue field);

        Task<bool> HashExistsAsync(RedisKey key, RedisValue field);

        Task<bool> SetAddAsync(RedisKey key, RedisValue value);

        Task<bool> SetContainsAsync(RedisKey key, RedisValue value);

        Task<bool> StringSetBitAsync(RedisKey key, long offset, bool bit = true);

        Task<long> UpdateCounterAsync(string keyName, int precision, long count = 1, DateTimeOffset? dateTime = null);

        Task<bool> UpdateCounterPrecisionAsync(string keyName, int precision);

        Task<long> GetGameUserSequentialIdAsync(string key);
    }
}