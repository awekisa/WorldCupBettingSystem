namespace BettingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserCompetitions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Competitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCompetition",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CompetitionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CompetitionId })
                .ForeignKey("dbo.Competitions", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CompetitionId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CompetitionId);
            
            AddColumn("dbo.Bets", "CompetitionId", c => c.Int());
            CreateIndex("dbo.Bets", "CompetitionId");
            AddForeignKey("dbo.Bets", "CompetitionId", "dbo.Competitions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCompetition", "CompetitionId", "dbo.Users");
            DropForeignKey("dbo.UserCompetition", "UserId", "dbo.Competitions");
            DropForeignKey("dbo.Bets", "CompetitionId", "dbo.Competitions");
            DropIndex("dbo.UserCompetition", new[] { "CompetitionId" });
            DropIndex("dbo.UserCompetition", new[] { "UserId" });
            DropIndex("dbo.Bets", new[] { "CompetitionId" });
            DropColumn("dbo.Bets", "CompetitionId");
            DropTable("dbo.UserCompetition");
            DropTable("dbo.Competitions");
        }
    }
}
