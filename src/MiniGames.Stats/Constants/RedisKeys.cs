using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Constants
{
    public static class RedisKeys
    { 
        public const string SystemName = "GameStats";
 
        public const string Users = SystemName + ":Users.Hash";
        
        public const string StartupCache = SystemName + ":StartupCache.List";

        public const string ActiveCache = SystemName + ":ActiveCache.List";
        
    }
}
