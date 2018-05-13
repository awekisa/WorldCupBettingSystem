using System.Data.Entity.ModelConfiguration;

namespace BettingSystem.Models.Configurations
{
    public class TeamConfiguration : EntityTypeConfiguration<Team>
    {
        public TeamConfiguration()
        {
            this.HasMany(e => e.HomeGames)
                .WithRequired(e => e.HomeTeam)
                .HasForeignKey(e => e.HomeTeamId)
                .WillCascadeOnDelete(false);

            this.HasMany(e => e.AwayGames)
                .WithRequired(e => e.AwayTeam)
                .HasForeignKey(e => e.AwayTeamId)
                .WillCascadeOnDelete(false);
        }
    }
}