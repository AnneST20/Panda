namespace Panda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChnageAdresssToCoordinated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ads", "Coordinates", c => c.String(nullable: false));
            DropColumn("dbo.Ads", "AdressLink");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "AdressLink", c => c.String(nullable: false));
            DropColumn("dbo.Ads", "Coordinates");
        }
    }
}
