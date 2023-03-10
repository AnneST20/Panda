namespace Panda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contextRefresh : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ads", "Url", c => c.String(nullable: true));
            AlterColumn("dbo.Ads", "SourceKey", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Price", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Adress", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "AdressLink", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Square", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Rooms", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Floor", c => c.String(nullable: false));
            AlterColumn("dbo.Ads", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ads", "Url", c => c.String());
            AlterColumn("dbo.Ads", "Description", c => c.String());
            AlterColumn("dbo.Ads", "Floor", c => c.String());
            AlterColumn("dbo.Ads", "Rooms", c => c.String());
            AlterColumn("dbo.Ads", "Square", c => c.String());
            AlterColumn("dbo.Ads", "AdressLink", c => c.String());
            AlterColumn("dbo.Ads", "Adress", c => c.String());
            AlterColumn("dbo.Ads", "Price", c => c.String());
            AlterColumn("dbo.Ads", "SourceKey", c => c.String());
        }
    }
}
