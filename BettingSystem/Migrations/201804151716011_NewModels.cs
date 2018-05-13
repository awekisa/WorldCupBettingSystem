namespace BettingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        HomeGoals = c.Int(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        HomeGoals = c.Int(),
                        AwayGoals = c.Int(),
                        IsOver = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Points", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bets", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Games", "AwayTeamId", "dbo.Teams");
            DropIndex("dbo.Games", new[] { "AwayTeamId" });
            DropIndex("dbo.Games", new[] { "HomeTeamId" });
            DropIndex("dbo.Bets", new[] { "UserId" });
            DropIndex("dbo.Bets", new[] { "GameId" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Users", "Points");
            DropTable("dbo.Teams");
            DropTable("dbo.Games");
            DropTable("dbo.Bets");
        }
    }
}
