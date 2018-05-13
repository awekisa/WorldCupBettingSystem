using System.Data.Entity.ModelConfiguration;

namespace BettingSystem.Models.Configurations
{
    public class CompetitionConfiguration : EntityTypeConfiguration<Competition>
    {
        public CompetitionConfiguration()
        {
            this.HasMany(e => e.Bets)
                .WithOptional(e => e.Competition)
                .HasForeignKey(e => e.CompetitionId);

            this.HasMany(e => e.Games)
                .WithOptional(e => e.Competition)
                .HasForeignKey(e => e.CompetitionId);

            this.HasMany(e => e.Users)
                .WithMany(e => e.Competitions)
                .Map(uc =>
                {
                    uc.MapLeftKey("UserId");
                    uc.MapRightKey("CompetitionId");
                    uc.ToTable("UserCompetition");
                });
        }
    }
}