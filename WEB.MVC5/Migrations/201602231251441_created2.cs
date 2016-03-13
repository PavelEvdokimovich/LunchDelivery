namespace WEB.MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isAdmin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "isUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isUser");
            DropColumn("dbo.Users", "isAdmin");
        }
    }
}
