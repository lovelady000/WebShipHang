namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableRegionAndAreas : DbMigration
    {
        public override void Up()
        {
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
                        RegionID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RegionID)
                .ForeignKey("dbo.Areas", t => t.RegionID)
                .Index(t => t.RegionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regions", "RegionID", "dbo.Areas");
            DropIndex("dbo.Regions", new[] { "RegionID" });
            DropTable("dbo.Regions");
            DropTable("dbo.Areas");
        }
    }
}
