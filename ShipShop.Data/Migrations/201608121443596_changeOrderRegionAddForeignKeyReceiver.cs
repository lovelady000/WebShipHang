namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOrderRegionAddForeignKeyReceiver : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Orders", "ReceiverRegionID");
            AddForeignKey("dbo.Orders", "ReceiverRegionID", "dbo.Regions", "RegionID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ReceiverRegionID", "dbo.Regions");
            DropIndex("dbo.Orders", new[] { "ReceiverRegionID" });
        }
    }
}
