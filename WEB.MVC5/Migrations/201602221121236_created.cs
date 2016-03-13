namespace WEB.MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Breads",
                c => new
                    {
                        BreadId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.BreadId);
            
            CreateTable(
                "dbo.Lunches",
                c => new
                    {
                        LunchId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        SaladId = c.Int(nullable: false),
                        SoupId = c.Int(nullable: false),
                        GarnishId = c.Int(nullable: false),
                        MeatDishId = c.Int(nullable: false),
                        BreadId = c.Int(nullable: false),
                        Menu = c.String(),
                        PriceWithSoup = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceWithoutSoup = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LunchId)
                .ForeignKey("dbo.Breads", t => t.BreadId, cascadeDelete: true)
                .ForeignKey("dbo.Garnishes", t => t.GarnishId, cascadeDelete: true)
                .ForeignKey("dbo.MeatDishes", t => t.MeatDishId, cascadeDelete: true)
                .ForeignKey("dbo.Salads", t => t.SaladId, cascadeDelete: true)
                .ForeignKey("dbo.Soups", t => t.SoupId, cascadeDelete: true)
                .Index(t => t.SaladId)
                .Index(t => t.SoupId)
                .Index(t => t.GarnishId)
                .Index(t => t.MeatDishId)
                .Index(t => t.BreadId);
            
            CreateTable(
                "dbo.Garnishes",
                c => new
                    {
                        GarnishId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.GarnishId);
            
            CreateTable(
                "dbo.MeatDishes",
                c => new
                    {
                        MeatDishId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.MeatDishId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        WithSoup = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        LunchId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Lunches", t => t.LunchId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LunchId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Salads",
                c => new
                    {
                        SaladId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.SaladId);
            
            CreateTable(
                "dbo.Soups",
                c => new
                    {
                        SoupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.SoupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lunches", "SoupId", "dbo.Soups");
            DropForeignKey("dbo.Lunches", "SaladId", "dbo.Salads");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "LunchId", "dbo.Lunches");
            DropForeignKey("dbo.Lunches", "MeatDishId", "dbo.MeatDishes");
            DropForeignKey("dbo.Lunches", "GarnishId", "dbo.Garnishes");
            DropForeignKey("dbo.Lunches", "BreadId", "dbo.Breads");
            DropIndex("dbo.Orders", new[] { "LunchId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Lunches", new[] { "BreadId" });
            DropIndex("dbo.Lunches", new[] { "MeatDishId" });
            DropIndex("dbo.Lunches", new[] { "GarnishId" });
            DropIndex("dbo.Lunches", new[] { "SoupId" });
            DropIndex("dbo.Lunches", new[] { "SaladId" });
            DropTable("dbo.Soups");
            DropTable("dbo.Salads");
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.MeatDishes");
            DropTable("dbo.Garnishes");
            DropTable("dbo.Lunches");
            DropTable("dbo.Breads");
        }
    }
}
