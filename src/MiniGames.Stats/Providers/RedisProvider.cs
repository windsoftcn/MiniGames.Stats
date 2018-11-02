using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace MiniGames.Stats.Providers
{
    public class RedisProvider : IRedisProvider
    {
        private readonly IConnectionMultiplexer connectionMultiplexer;

        public RedisProvider(IConnectionMultiplexer connectionMultiplexer)
        {
            this.connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
        }

        private IDatabase database;

        public IDatabase Database => database ?? (database = connectionMultiplexer.GetDatabase());

        public Task<RedisValue> StringGetAsync(RedisKey key) => Database.StringGetAsync(key);

        public Task<long> StringIncrementAsync(RedisKey key) => Database.StringIncrementAsync(key);

        public Task<bool> StringSetAsync(RedisKey key, RedisValue value) => Database.StringSetAsync(key, value);

        public Task<bool> HashSetAsync(RedisKey key, RedisValue field, RedisValue value) => Database.HashSetAsync(key, field, value);

        public Task<RedisValue> HashGetAsync(RedisKey key, RedisValue field) => Database.HashGetAsync(key, field);

        public Task<bool> HashExistsAsync(RedisKey key, RedisValue field) => Database.HashExistsAsync(key, field);

        public Task<bool> SetAddAsync(RedisKey key, RedisValue value) => Database.SetAddAsync(key, value);

        public Task<bool> SetContainsAsync(RedisKey key, RedisValue value) => Database.SetContainsAsync(key, value);

        public Task<bool> StringSetBitAsync(RedisKey key, long offset, bool bit = true) => Database.StringSetBitAsync(key, offset, bit);

        public Task<long> UpdateCounterAsync(string keyName, int precision, long count = 1, DateTimeOffset? dateTime = null)
        {
            long unixTimeSeconds = (dateTime ?? DateTimeOffset.Now).ToUnixTimeSeconds();
            long countTime = (unixTimeSeconds / precision) * precision;            
            // 记录日志
           return Database.HashIncrementAsync($"{keyName}:{precision}", countTime, count);
        }

        public Task<bool> UpdateCounterPrecisionAsync(string keyName, int precision)
        {
            // 添加精度集合, 用于筛选过滤
            return Database.SortedSetAddAsync($"{keyName}:precisions", precision, 0);
        }


        private readonly object _gameUserSequentialIdSyncLock = new object();

        public Task<long> GetGameUserSequentialIdAsync(string key)
        {
            lock (_gameUserSequentialIdSyncLock)
            {
                return Database.StringIncrementAsync(key);
            }
        }
         
    }
}