namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplUser2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUsers", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUsers", "IsAdmin", c => c.Boolean());
        }
    }
}
