namespace Panda.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contextSettings : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ads", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Ads", new[] { "User_Id" });
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        AdId = c.String(maxLength: 128),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ads", t => t.AdId)
                .Index(t => t.AdId);
            
            AddColumn("dbo.Ads", "PetsAllowed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.LikedAds", "AdId", c => c.String(maxLength: 128));
            AlterColumn("dbo.LikedAds", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.LikedAds", "AdId");
            CreateIndex("dbo.LikedAds", "UserId");
            AddForeignKey("dbo.LikedAds", "AdId", "dbo.Ads", "Id");
            AddForeignKey("dbo.LikedAds", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Ads", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ads", "User_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.LikedAds", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikedAds", "AdId", "dbo.Ads");
            DropForeignKey("dbo.Photos", "AdId", "dbo.Ads");
            DropIndex("dbo.LikedAds", new[] { "UserId" });
            DropIndex("dbo.LikedAds", new[] { "AdId" });
            DropIndex("dbo.Photos", new[] { "AdId" });
            AlterColumn("dbo.LikedAds", "UserId", c => c.String());
            AlterColumn("dbo.LikedAds", "AdId", c => c.String());
            DropColumn("dbo.Ads", "PetsAllowed");
            DropTable("dbo.Photos");
            CreateIndex("dbo.Ads", "User_Id");
            AddForeignKey("dbo.Ads", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
