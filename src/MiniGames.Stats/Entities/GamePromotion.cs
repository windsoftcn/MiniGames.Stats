using System;

namespace MiniGames.Stats.Entities
{
    public class GamePromotion : BaseEntity
    {
        public int GameAppId { get; set; }
        public GameApp GameApp { get; set; }

        public int MerchantId { get; set; }
        public Merchant Merchant { get;set; }
                
        // уш©ш
        public decimal? DiscountRate { get; set; } 
        public decimal? DiscountInitial { get; set; }
        public bool DiscountEnbaled { get; set; }        
    }
}