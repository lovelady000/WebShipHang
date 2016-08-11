namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicationUserAddWebInformation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Regions", "AreaID", "dbo.Areas");
            DropIndex("dbo.Regions", new[] { "AreaID" });
            CreateTable(
                "dbo.WebInformations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Slogan = c.String(),
                        HotLine = c.String(),
                        Skyper = c.String(),
                        Facebook = c.String(),
                        LinkAppIOS = c.String(),
                        LinkAppAndroid = c.String(),
                        LinkAppWindowPhone = c.String(),
                        Longitude = c.String(),
                        Latitude = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ApplicationUsers", "Vendee", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApplicationUsers", "WebOrShopName", c => c.String());
            AddColumn("dbo.ApplicationUsers", "Adress", c => c.String());
            AlterColumn("dbo.ApplicationUsers", "Address", c => c.String());
            DropTable("dbo.Areas");
            DropTable("dbo.Regions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AreaID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.AreaID);
            
            AlterColumn("dbo.ApplicationUsers", "Address", c => c.String(maxLength: 256));
            DropColumn("dbo.ApplicationUsers", "Adress");
            DropColumn("dbo.ApplicationUsers", "WebOrShopName");
            DropColumn("dbo.ApplicationUsers", "Vendee");
            DropTable("dbo.WebInformations");
            CreateIndex("dbo.Regions", "AreaID");
            AddForeignKey("dbo.Regions", "AreaID", "dbo.Areas", "AreaID", cascadeDelete: true);
        }
    }
}
