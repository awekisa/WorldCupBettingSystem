using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.Configurations
{
    public class GameConfiguration : EntityTypeConfiguration<Game>
    {
        public GameConfiguration()
        {
            this.HasMany(e => e.Bets)
                .WithRequired(e => e.Game)
                .HasForeignKey(e => e.GameId);
        }
    }
}