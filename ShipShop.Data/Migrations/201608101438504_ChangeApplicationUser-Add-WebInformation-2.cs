namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicationUserAddWebInformation2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderDetailID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderDetailID" });
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.AreaID);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionID = c.Int(nullable: false, identity: true),
                        AreaID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RegionID)
                .ForeignKey("dbo.Areas", t => t.AreaID, cascadeDelete: true)
                .Index(t => t.AreaID);
            
            AddColumn("dbo.ApplicationUsers", "RegionID", c => c.Int(nullable: false));
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SenderName = c.String(nullable: false, maxLength: 256),
                        SenderAddress = c.String(nullable: false),
                        SenderMobile = c.String(nullable: false, maxLength: 50),
                        SenderRegion = c.String(),
                        ReceiverName = c.String(nullable: false, maxLength: 256),
                        ReceiverAddress = c.String(nullable: false),
                        ReceiverMobile = c.String(nullable: false, maxLength: 50),
                        ReceiverRegion = c.String(),
                        PaymentMethod = c.String(maxLength: 256),
                        PayCOD = c.Single(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderDetailID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        NameProduct = c.String(maxLength: 256),
                        UrlProductDetail = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.OrderDetailID);
            
            DropForeignKey("dbo.Regions", "AreaID", "dbo.Areas");
            DropIndex("dbo.Regions", new[] { "AreaID" });
            DropColumn("dbo.ApplicationUsers", "RegionID");
            DropTable("dbo.Regions");
            DropTable("dbo.Areas");
            CreateIndex("dbo.OrderDetails", "OrderDetailID");
            AddForeignKey("dbo.OrderDetails", "OrderDetailID", "dbo.Orders", "ID");
        }
    }
}
