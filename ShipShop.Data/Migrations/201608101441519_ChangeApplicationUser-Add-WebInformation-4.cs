namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicationUserAddWebInformation4 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.ApplicationUsers", "RegionID");
            AddForeignKey("dbo.ApplicationUsers", "RegionID", "dbo.Regions", "RegionID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUsers", "RegionID", "dbo.Regions");
            DropIndex("dbo.ApplicationUsers", new[] { "RegionID" });
        }
    }
}
