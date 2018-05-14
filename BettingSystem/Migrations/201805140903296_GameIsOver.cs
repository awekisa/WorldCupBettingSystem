namespace BettingSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GameIsOver : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "IsOver");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "IsOver", c => c.Boolean(nullable: false));
        }
    }
}
