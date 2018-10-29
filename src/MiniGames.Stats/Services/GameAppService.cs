using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MiniGames.Stats.Constants;
using MiniGames.Stats.Data;
using MiniGames.Stats.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Services
{
    public class GameAppService : IGameAppService
    {
        private readonly IMemoryCache memoryCache;
        private readonly GameDbContext dbContext;

        public GameAppService(IMemoryCache memoryCache,GameDbContext dbContext)
        {
            this.memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        private IList<GameApp> _cachedGameApps;
        public IList<GameApp> CachedGameApps => _cachedGameApps ?? (_cachedGameApps = memoryCache.Get<IList<GameApp>>(MemoryKeys.AllGameApps));


        public Task<List<GameApp>> GetAllGameAppsAsync() => dbContext.GameApps.ToListAsync();
               
        public async Task<bool> GameAppExistsAsync(string appId)
        {
            if (CachedGameApps.Any(g => g.AppId.Equals(appId)))
            {
                return true;
            }
                        
            List<GameApp> allGameApps = await GetAllGameAppsAsync();

            if (allGameApps.Any(g => g.AppId.Equals(appId)))
            {
                memoryCache.Set<IList<GameApp>>(MemoryKeys.AllGameApps, allGameApps);
                return true;
            }
            return false;
        }

        public async Task<bool> AddGameAppAsync(GameApp gameApp)
        {
            if(await GameAppExistsAsync(gameApp.AppId))
            {
                return false;
            }

            dbContext.Add(gameApp);

            return await dbContext.SaveChangesAsync() > 0;
        }        
    }
}
