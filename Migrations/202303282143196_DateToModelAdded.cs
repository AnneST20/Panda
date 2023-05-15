namespace Panda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateToModelAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "ChildrenAllowed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ads", "PublicationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Ads", "SaveToContextDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Ads", "ChaldrenAllowed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "ChaldrenAllowed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Ads", "SaveToContextDate");
            DropColumn("dbo.Ads", "PublicationDate");
            DropColumn("dbo.Ads", "ChildrenAllowed");
        }
    }
}
