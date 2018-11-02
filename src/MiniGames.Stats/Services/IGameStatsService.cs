using System;
using System.Threading.Tasks;

namespace MiniGames.Stats.Services
{
    public interface IGameStatsService
    {
        Task GameUserLoginAsync(string gameAppId, string gameUserId, DateTimeOffset? dateTime);
                
        Task<bool> GameUserExistsAsync(string gameAppId, string gameUserId);

        Task<long> GetGameUserUserSequentialIdAsync(string gameAppId, string gameUserId);

        Task<bool> AddGameUserSetAsync(string gameAppId, string gameUserId);

        Task<bool> AddGameUserHashAsync(string gameAppId, string gameUserId, long gameUserSequentialId);

    }
}