namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogoWebInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebInformations", "Logo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebInformations", "Logo");
        }
    }
}
