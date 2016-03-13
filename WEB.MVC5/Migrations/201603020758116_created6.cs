namespace WEB.MVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class created6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        LunchId = c.Int(nullable: false),
                        SaladRating = c.Int(nullable: true),
                        SaladComment = c.String(),
                        SoupRating = c.Int(nullable: true),
                        SoupComment = c.String(),
                        MeatDishRating = c.Int(nullable: true),
                        MeatDishComment = c.String(),
                        GarnishRating = c.Int(nullable: true),
                        GarnishComment = c.String(),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Lunches", t => t.LunchId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LunchId);
            
            AddColumn("dbo.Garnishes", "Rating", c => c.Double(nullable: true));
            AddColumn("dbo.MeatDishes", "Rating", c => c.Double(nullable: true));
            AddColumn("dbo.Salads", "Rating", c => c.Double(nullable: true));
            AddColumn("dbo.Soups", "Rating", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "LunchId", "dbo.Lunches");
            DropIndex("dbo.Comments", new[] { "LunchId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropColumn("dbo.Soups", "Rating");
            DropColumn("dbo.Salads", "Rating");
            DropColumn("dbo.MeatDishes", "Rating");
            DropColumn("dbo.Garnishes", "Rating");
            DropTable("dbo.Comments");
        }
    }
}
