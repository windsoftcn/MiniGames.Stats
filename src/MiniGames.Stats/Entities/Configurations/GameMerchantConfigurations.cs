﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGames.Stats.Entities.Configurations
{
    public class GameMerchantConfigurations : IEntityTypeConfiguration<GameMerchant>
    {
        public void Configure(EntityTypeBuilder<GameMerchant> builder)
        {
            throw new NotImplementedException();
        }
    }
}
