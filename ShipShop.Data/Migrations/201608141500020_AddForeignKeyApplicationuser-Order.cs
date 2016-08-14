namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyApplicationuserOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "UserID");
            AddForeignKey("dbo.Orders", "UserID", "dbo.ApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.ApplicationUsers");
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropColumn("dbo.Orders", "UserID");
        }
    }
}
