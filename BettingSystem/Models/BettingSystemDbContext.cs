using BettingSystem.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BettingSystem.Models
{
    public class BettingSystemDbContext : DbContext
    {
        public BettingSystemDbContext() : base("name=DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Competition> Competitions { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new UserConfiguration());
            builder.Configurations.Add(new TeamConfiguration());
            builder.Configurations.Add(new BetConfiguration());
            builder.Configurations.Add(new GameConfiguration());
            builder.Configurations.Add(new CompetitionConfiguration());
        }
    }
}