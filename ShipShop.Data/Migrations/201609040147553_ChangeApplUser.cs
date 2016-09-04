namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "IsAdmin", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "IsAdmin");
        }
    }
}
