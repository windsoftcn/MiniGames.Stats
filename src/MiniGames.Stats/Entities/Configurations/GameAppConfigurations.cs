using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Entities.Configurations
{
    public class GameAppConfigurations : IEntityTypeConfiguration<GameApp>
    {
        public void Configure(EntityTypeBuilder<GameApp> builder)
        {
            builder.HasKey(e => e.AppId);
            builder.Property(e => e.AppId)
                .HasMaxLength(36)
                .IsRequired()
                .ValueGeneratedNever();


            builder.HasData(
                new GameApp { AppId = "TestAppId0123456789", Key = "SecurityKey", Name = "TestAppId" });

        }
    }
}
