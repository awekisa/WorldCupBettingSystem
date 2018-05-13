namespace BettingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bets : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Bets", new[] { "GameId" });
            DropIndex("dbo.Bets", new[] { "UserId" });
            CreateIndex("dbo.Bets", new[] { "UserId", "GameId" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Bets", new[] { "UserId", "GameId" });
            CreateIndex("dbo.Bets", "UserId");
            CreateIndex("dbo.Bets", "GameId");
        }
    }
}
