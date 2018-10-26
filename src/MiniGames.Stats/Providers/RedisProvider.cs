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

        public async Task<RedisValue> StringGetAsync(RedisKey key) => await Database.StringGetAsync(key);

        public async Task<long> StringIncrementAsync(RedisKey key) => await Database.StringIncrementAsync(key);

        public async Task<bool> StringSetAsync(RedisKey key, RedisValue value) => await Database.StringSetAsync(key, value);

        public async Task<bool> HashSetAsync(RedisKey key, RedisValue field, RedisValue value) => await Database.HashSetAsync(key, field, value);

        public async Task<RedisValue> HashGetAsync(RedisKey key, RedisValue field) => await Database.HashGetAsync(key, field);

        public async Task<bool> HashExistsAsync(RedisKey key, RedisValue field) => await Database.HashExistsAsync(key, field);
    }
}