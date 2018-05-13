using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.Configurations
{
    public class BetConfiguration : EntityTypeConfiguration<Bet>
    {
        public BetConfiguration()
        {
            this.HasIndex(b => new { b.UserId, b.GameId })
                .IsUnique(true);
        }
    }
}