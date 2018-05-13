namespace BettingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NonNullGoals : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "HomeGoals", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "AwayGoals", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "AwayGoals", c => c.Int());
            AlterColumn("dbo.Games", "HomeGoals", c => c.Int());
        }
    }
}
