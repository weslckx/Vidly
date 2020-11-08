namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedBugMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "Title", c => c.String(nullable: false));
            DropColumn("dbo.Movies", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Name", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "Title");
        }
    }
}
