namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicationUserAddWebInformation3 : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.OrderDetailID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
        }
    }
}
