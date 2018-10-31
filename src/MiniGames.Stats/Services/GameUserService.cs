using MiniGames.Stats.Constants;
using MiniGames.Stats.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Services
{
    public class GameUserService : IGameUserService
    {
        private readonly IRedisProvider redisProvider;

        public GameUserService(IRedisProvider redisProvider)
        {
            this.redisProvider = redisProvider ?? throw new ArgumentNullException(nameof(redisProvider));
        }

        public async Task AddUserLoginCountAsync(string userId, DateTimeOffset? dateTime = null)
        {
            // 验证用户是否存在
            long userSequentialId = await GetUserSequentialIdAsync(userId);

            if (userSequentialId == default(long))
            {
                // 添加新用户
                userSequentialId = await AddNewUserAsync(userId, dateTime);
                if (userSequentialId > 0)
                {
                    await DailyNewUserCountAsync(userSequentialId, dateTime);

                    await HourlyNewUserCountAsync(userSequentialId, dateTime);
                }
            }
            
            // 记录活跃 / 天
            await UserActiveCountAsync(userSequentialId, dateTime);

            // 记录登录 / 天
            await DailyLoginCountAsync(userSequentialId, dateTime);

            // 记录登录 / 时
            await HourlyLoginCountAsync(userSequentialId, dateTime);            
        }


        public async Task<long> AddNewUserAsync(string userId, DateTimeOffset? dateTime = null)
        {
            var sequentialId = await redisProvider.StringIncrementAsync(RedisKeys.SequentialId);
            if(sequentialId > 0)
            {
               if( await redisProvider.HashSetAsync(RedisKeys.Users, userId, sequentialId))
                {
                    return sequentialId;
                }
            }
            return 0;
        }

        public Task<bool> DailyNewUserCountAsync(long userSequentialId, DateTimeOffset? dateTime = null)
        {            
           return redisProvider.Database.StringSetBitAsync(RedisKeys.GetDailyNewUserCountKey(dateTime), userSequentialId, true);
        }

        public Task<bool> HourlyNewUserCountAsync(long userSequentialId, DateTimeOffset? dateTime = null)
        {
            return redisProvider.Database.StringSetBitAsync(RedisKeys.GetHourlyNewUserCountKey(dateTime), userSequentialId, true);
        }

        public Task<bool> DailyLoginCountAsync(long userSequentialId, DateTimeOffset? dateTime = null)
        {
            return redisProvider.Database.StringSetBitAsync(RedisKeys.GetHourlyNewUserCountKey(dateTime), userSequentialId, true);
        }

        public Task<bool> HourlyLoginCountAsync(long userSequentialId, DateTimeOffset? dateTime = null)
        {
            return redisProvider.Database.StringSetBitAsync(RedisKeys.GetHourlyNewUserCountKey(dateTime), userSequentialId, true);
        }

        public Task<bool> UserActiveCountAsync(long userSequentialId, DateTimeOffset? dateTime = null)
        {
            return redisProvider.Database.StringSetBitAsync(RedisKeys.GetHourlyNewUserCountKey(dateTime), userSequentialId, true);
        }

        public async Task<long> GetUserSequentialIdAsync(string userId)
        {
            var redisValue = await redisProvider.HashGetAsync(RedisKeys.Users, userId);
            if(redisValue.HasValue && redisValue.IsInteger)
            {
                return (long)redisValue;
            }
            return default(long);
        }
    }
}
