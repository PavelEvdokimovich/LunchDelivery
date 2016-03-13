namespace WEB.MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PhotoData", c => c.Binary());
            AddColumn("dbo.Users", "PhotoMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhotoMimeType");
            DropColumn("dbo.Users", "PhotoData");
        }
    }
}
