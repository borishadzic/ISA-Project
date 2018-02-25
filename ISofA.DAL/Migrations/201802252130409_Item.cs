namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemId = c.Guid(nullable: false, identity: true),
                        TheaterId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Single(nullable: false),
                        ImageUrl = c.String(),
                        BuyerId = c.String(maxLength: 128),
                        BoughtDate = c.DateTime(precision: 0, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerId)
                .Index(t => t.TheaterId)
                .Index(t => t.BuyerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "BuyerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "TheaterId", "dbo.Theaters");
            DropIndex("dbo.Items", new[] { "BuyerId" });
            DropIndex("dbo.Items", new[] { "TheaterId" });
            DropTable("dbo.Items");
        }
    }
}
