namespace Panda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ads", "Price", c => c.String());
            AlterColumn("dbo.Ads", "Adress", c => c.String());
            AlterColumn("dbo.Ads", "Coordinates", c => c.String());
            AlterColumn("dbo.Ads", "Square", c => c.String());
            AlterColumn("dbo.Ads", "Rooms", c => c.String());
            AlterColumn("dbo.Ads", "Floor", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ads", "Floor", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Rooms", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Square", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Coordinates", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Adress", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Price", c => c.String(nullable: false));
        }
    }
}
