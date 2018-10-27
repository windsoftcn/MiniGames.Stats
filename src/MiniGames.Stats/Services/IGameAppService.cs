using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Services
{
    public interface IGameAppService
    {
        Task<bool> GameAppExistsAsync(string appId);
    }
}
