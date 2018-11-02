using MiniGames.Stats.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGames.Stats.Services
{
    public class GameStatsService : IGameStatsService
    {
        private readonly IRedisProvider redisProvider;

        public GameStatsService(IRedisProvider redisProvider)
        {
            this.redisProvider = redisProvider ?? throw new ArgumentNullException(nameof(redisProvider));
        }

        public async Task GameUserLoginAsync(string gameAppId, string gameUserId, DateTimeOffset? dateTime)
        {
            int[] timePrecisions = new int[] { 3600, 3600 * 24 };
            // 查找用户是否存在
            if (!await GameUserExistsAsync(gameAppId, gameUserId))
            {
                // 添加新用户
                string sequentialKey = RedisKeys.Concat(gameAppId, RedisKeys.UserSequentialId);
                var gameUserSequentialId = await redisProvider.GetGameUserSequentialIdAsync(sequentialKey);             
                
                await AddGameUserSetAsync(gameAppId, gameUserId).ConfigureAwait(false);
                await AddGameUserHashAsync(gameAppId, gameUserId, gameUserSequentialId).ConfigureAwait(false);

                // 统计新增用户                  
                string newUserKey = RedisKeys.Concat(gameAppId, RedisKeys.NewGameUserCount);
                foreach (var precision in timePrecisions)
                {
                    await redisProvider.UpdateCounterAsync(newUserKey, precision).ConfigureAwait(false);
                    await redisProvider.UpdateCounterPrecisionAsync(newUserKey, precision).ConfigureAwait(false);
                }
            }
            // 统计用户登录            
            string loginKey = RedisKeys.Concat(gameAppId, RedisKeys.GameUserLoginCount);
            foreach (var precision in timePrecisions)
            {
                await redisProvider.UpdateCounterAsync(loginKey, precision).ConfigureAwait(false);
                await redisProvider.UpdateCounterPrecisionAsync(loginKey, precision).ConfigureAwait(false);
            }

            // 统计用户活跃
            await GameUserActiveAsync(gameAppId, gameUserId, dateTime).ConfigureAwait(false);
        }


        public async Task GameUserActiveAsync(string gameAppId, string gameUserId, DateTimeOffset? dateTime)
        {
            long gameUserSequentialId = default(long);
            // 查找用户Id
            if (gameUserSequentialId == default(long))
            {
                gameUserSequentialId = await GetGameUserUserSequentialIdAsync(gameAppId, gameUserId);
            }

            // 添加用户活跃标志位
            await GameUserActiveAsync(gameAppId, gameUserSequentialId, dateTime);
        }

        public Task<bool> GameUserExistsAsync(string gameAppId, string gameUserId)
        {
            string key = RedisKeys.Concat(gameAppId, RedisKeys.UserSet); 

            return redisProvider.SetContainsAsync(key, gameUserId);
        }

        public async Task<long> GetGameUserUserSequentialIdAsync(string gameAppId, string gameUserId)
        {
            string key = RedisKeys.Concat(gameAppId, RedisKeys.UserHash); 
            var redisValue = await  redisProvider.HashGetAsync(key, gameUserId);
            if(redisValue.HasValue && redisValue.IsInteger)
            {
                return (long)redisValue;
            }
            return default(long);
        }

        public Task<bool> AddGameUserSetAsync(string gameAppId, string gameUserId)
        {
            // 添加用户到Set
            string userSetKey = RedisKeys.Concat(gameAppId, RedisKeys.UserSet);
            return redisProvider.SetAddAsync(userSetKey, gameUserId);
        }

        public Task<bool> AddGameUserHashAsync(string gameAppId, string gameUserId, long gameUserSequentialId)
        {    
            // 添加用户到UserHash
            string userHashKey = RedisKeys.Concat(gameAppId, RedisKeys.UserHash);
            return redisProvider.HashSetAsync(userHashKey, gameUserId, gameUserSequentialId);             
        }

        public Task<bool> GameUserActiveAsync(string gameAppId, long gameUserSequentialId,  DateTimeOffset? dateTime)
        {
            if (gameUserSequentialId <= 0)
                throw new ArgumentException("gameUserSequentialId should larger than 0");

            string activeTrackerKey = RedisKeys.Concat(gameAppId, RedisKeys.GameUserActiveTracker, (dateTime ?? DateTimeOffset.Now).ToString("yyyyMMdd"));

            return redisProvider.StringSetBitAsync(activeTrackerKey, gameUserSequentialId);
        }

        

        
    }

    public struct RedisKeys
    {
        public const string SystemName = "GameStats";

        public const string UserSet = "UserSet";

        public const string UserHash = "UserHash";

        public const string UserSequentialId = "SequentialIds:User";

        public const string NewGameUserCount = "NewUserCount";

        public const string GameUserLoginCount = "LoginCount";

        public const string GameUserActiveCount = "ActiveCount";

        public const string GameUserActiveTracker = "ActiveTracker";

        private const string RedisSeparator = ":";
        
        public static string Concat(params string[] values)
        {
            StringBuilder builder = new StringBuilder(SystemName);

            foreach(var value in values)
            {
                builder.Append(RedisSeparator).Append(value);
            }

            return builder.ToString();
        }
    }
}
