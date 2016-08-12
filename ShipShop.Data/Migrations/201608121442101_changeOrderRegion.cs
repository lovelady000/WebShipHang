namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOrderRegion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SenderRegionID", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ReceiverRegionID", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "SenderRegion");
            DropColumn("dbo.Orders", "ReceiverRegion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ReceiverRegion", c => c.String());
            AddColumn("dbo.Orders", "SenderRegion", c => c.String());
            DropColumn("dbo.Orders", "ReceiverRegionID");
            DropColumn("dbo.Orders", "SenderRegionID");
        }
    }
}
