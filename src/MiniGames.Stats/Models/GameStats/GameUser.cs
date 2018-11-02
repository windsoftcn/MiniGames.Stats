using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Models.GameStats
{
    public class GameUser
    {
        public int Id { get; set; }

        public string GameAppId { get; set; }

        public string OpenId { get; set; }

        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;
        
    }
}
