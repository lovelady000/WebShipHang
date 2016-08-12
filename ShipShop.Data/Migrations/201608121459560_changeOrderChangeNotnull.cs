namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeOrderChangeNotnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "SenderName", c => c.String(maxLength: 256));
            AlterColumn("dbo.Orders", "ReceiverName", c => c.String(maxLength: 256));
            AlterColumn("dbo.Orders", "PayCOD", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PayCOD", c => c.Single(nullable: false));
            AlterColumn("dbo.Orders", "ReceiverName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Orders", "SenderName", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
