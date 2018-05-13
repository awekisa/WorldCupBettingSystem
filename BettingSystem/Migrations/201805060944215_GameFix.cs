namespace BettingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "CompetitionId", c => c.Int());
            CreateIndex("dbo.Games", "CompetitionId");
            AddForeignKey("dbo.Games", "CompetitionId", "dbo.Competitions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "CompetitionId", "dbo.Competitions");
            DropIndex("dbo.Games", new[] { "CompetitionId" });
            DropColumn("dbo.Games", "CompetitionId");
        }
    }
}
