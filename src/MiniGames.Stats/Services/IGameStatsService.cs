using System.Threading.Tasks;

namespace MiniGames.Stats.Services
{
    public interface IGameStatsService
    {
        Task<bool> GameUserExistsAsync(string openId);

        
    }
}