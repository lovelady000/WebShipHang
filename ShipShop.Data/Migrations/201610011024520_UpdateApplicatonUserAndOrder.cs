namespace ShipShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicatonUserAndOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "IsBanded", c => c.Boolean());
            AddColumn("dbo.Orders", "SenderView", c => c.Boolean());
            AddColumn("dbo.Orders", "ReceiverView", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ReceiverView");
            DropColumn("dbo.Orders", "SenderView");
            DropColumn("dbo.ApplicationUsers", "IsBanded");
        }
    }
}
