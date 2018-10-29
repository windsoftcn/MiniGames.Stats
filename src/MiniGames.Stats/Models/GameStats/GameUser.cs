using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Models.GameStats
{
    public class GameUser
    {
        public GameUser()
        {
            CreateTime = DateTimeOffset.Now;
        }

        public string OpenId { get; set; }

        public DateTimeOffset CreateTime { get; set; } 
                
        public string CameFrom { get; set; }
                
    }
}
