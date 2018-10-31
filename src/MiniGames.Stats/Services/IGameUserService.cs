using System;
using System.Threading.Tasks;

namespace MiniGames.Stats.Services
{
    public interface IGameUserService
    {
        Task AddUserLoginCountAsync(string userId, DateTimeOffset? dateTime = null);

        Task<long> AddNewUserAsync(string userId, DateTimeOffset? dateTime = null);

        Task<bool> DailyNewUserCountAsync(long userSequentialId, DateTimeOffset? dateTime = null);

        Task<bool> HourlyNewUserCountAsync(long userSequentialId, DateTimeOffset? dateTime = null);

        Task<bool> DailyLoginCountAsync(long userSequentialId, DateTimeOffset? dateTime = null);

        Task<bool> HourlyLoginCountAsync(long userSequentialId, DateTimeOffset? dateTime = null);
        Task<bool> UserActiveCountAsync(long userSequentialId, DateTimeOffset? dateTime = null);

        Task<long> GetUserSequentialIdAsync(string userId);
      

    }
}