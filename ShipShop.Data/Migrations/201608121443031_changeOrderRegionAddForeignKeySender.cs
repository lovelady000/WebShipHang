namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOrderRegionAddForeignKeySender : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "SenderRegionID");
            AddForeignKey("dbo.Orders", "SenderRegionID", "dbo.Regions", "RegionID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "SenderRegionID", "dbo.Regions");
            DropIndex("dbo.Orders", new[] { "SenderRegionID" });
        }
    }
}
