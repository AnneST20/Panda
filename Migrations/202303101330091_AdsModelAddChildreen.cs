namespace Panda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdsModelAddChildreen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "ChaldrenAllowed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Ads", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ads", "Description", c => c.String(nullable: false));
            DropColumn("dbo.Ads", "ChaldrenAllowed");
        }
    }
}
