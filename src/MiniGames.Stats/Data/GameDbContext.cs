using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniGames.Stats.Entities;
using MiniGames.Stats.Entities.Configurations;
using System;

namespace MiniGames.Stats.Data
{
    public class GameDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<GameApp> GameApps { get; set; }

        public DbSet<Merchant> Merchant { get; set; }

        public DbSet<GamePromotion> GameMerchant { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new GameAppConfigurations());
                
            
        }
    }
}