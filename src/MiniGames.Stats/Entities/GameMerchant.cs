using System;

namespace MiniGames.Stats.Entities
{
    public class GameMerchant : BaseEntity
    {
        public int GameAppId { get; set; }
        public GameApp GameApp { get; set; }

        public int MerchantId { get; set; }
        public Merchant Merchant { get;set; }
    }
}