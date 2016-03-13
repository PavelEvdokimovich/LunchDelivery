namespace WEB.MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        BalanceId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Income = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Expence = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.BalanceId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            DropColumn("dbo.Orders", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Balances", "UserId", "dbo.Users");
            DropIndex("dbo.Balances", new[] { "UserId" });
            DropTable("dbo.Balances");
        }
    }
}
