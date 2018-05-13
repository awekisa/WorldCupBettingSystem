using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace BettingSystem.Models.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasMany(e => e.Bets)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId); 
        }
    }
}