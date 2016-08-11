namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicationUserAddWebInformation5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationUsers", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUsers", "Adress", c => c.String());
        }
    }
}
