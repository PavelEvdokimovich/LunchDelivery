namespace WEB.MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "WithoutSoup", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Delivered", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "WithSoup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "WithSoup", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "Delivered");
            DropColumn("dbo.Orders", "WithoutSoup");
        }
    }
}
