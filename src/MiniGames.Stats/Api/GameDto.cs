using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Api
{
    public class GameDto
    {
        public string AppId { get; set; }
        
        public string Version { get; set; }

        public string From { get; set; }

        public string SharedBy { get; set; }
    }
}
