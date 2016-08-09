namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModelsAreaRegion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Regions", "RegionID", "dbo.Areas");
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Regions", new[] { "RegionID" });
            DropPrimaryKey("dbo.OrderDetails");
            DropPrimaryKey("dbo.Regions");
            AddColumn("dbo.OrderDetails", "OrderDetailID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Regions", "AreaID", c => c.Int(nullable: false));
            AlterColumn("dbo.Regions", "RegionID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.OrderDetails", "OrderDetailID");
            AddPrimaryKey("dbo.Regions", "RegionID");
            CreateIndex("dbo.OrderDetails", "OrderDetailID");
            CreateIndex("dbo.Regions", "AreaID");
            AddForeignKey("dbo.OrderDetails", "OrderDetailID", "dbo.Orders", "ID");
            AddForeignKey("dbo.Regions", "AreaID", "dbo.Areas", "AreaID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regions", "AreaID", "dbo.Areas");
            DropForeignKey("dbo.OrderDetails", "OrderDetailID", "dbo.Orders");
            DropIndex("dbo.Regions", new[] { "AreaID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderDetailID" });
            DropPrimaryKey("dbo.Regions");
            DropPrimaryKey("dbo.OrderDetails");
            AlterColumn("dbo.Regions", "RegionID", c => c.Int(nullable: false));
            DropColumn("dbo.Regions", "AreaID");
            DropColumn("dbo.OrderDetails", "OrderDetailID");
            AddPrimaryKey("dbo.Regions", "RegionID");
            AddPrimaryKey("dbo.OrderDetails", "OrderID");
            CreateIndex("dbo.Regions", "RegionID");
            CreateIndex("dbo.OrderDetails", "OrderID");
            AddForeignKey("dbo.Regions", "RegionID", "dbo.Areas", "AreaID");
            AddForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders", "ID");
        }
    }
}
