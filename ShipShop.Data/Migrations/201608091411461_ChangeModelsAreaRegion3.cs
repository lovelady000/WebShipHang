namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeModelsAreaRegion3 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Regions");
            AddColumn("dbo.Regions", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Regions", "ID");
            DropColumn("dbo.Regions", "RegionID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Regions", "RegionID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Regions");
            DropColumn("dbo.Regions", "ID");
            AddPrimaryKey("dbo.Regions", "RegionID");
        }
    }
}
