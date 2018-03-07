namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserItemAndBid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "TheaterId", "dbo.Theaters");
            CreateTable(
                "dbo.UserItems",
                c => new
                    {
                        UserItemId = c.Guid(nullable: false, identity: true),
                        ISofAUserId = c.String(nullable: false, maxLength: 128),
                        TheaterId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                        Approved = c.Boolean(),
                        Sold = c.Boolean(),
                        HighestBid = c.Single(),
                        HighestBidderId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserItemId)
                .ForeignKey("dbo.AspNetUsers", t => t.HighestBidderId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .ForeignKey("dbo.AspNetUsers", t => t.ISofAUserId)
                .Index(t => t.ISofAUserId)
                .Index(t => t.TheaterId)
                .Index(t => t.HighestBidderId);
            
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        BidId = c.Guid(nullable: false, identity: true),
                        BidDate = c.DateTime(nullable: false),
                        BidderId = c.String(nullable: false, maxLength: 128),
                        UserItemId = c.Guid(nullable: false),
                        BidAmount = c.Single(nullable: false),
                        Won = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BidId)
                .ForeignKey("dbo.UserItems", t => t.UserItemId)
                .ForeignKey("dbo.AspNetUsers", t => t.BidderId)
                .Index(t => t.BidderId)
                .Index(t => t.UserItemId);
            
            AlterColumn("dbo.Items", "BoughtDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddForeignKey("dbo.Items", "TheaterId", "dbo.Theaters", "TheaterId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.UserItems", "ISofAUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "BidderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserItems", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.UserItems", "HighestBidderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "UserItemId", "dbo.UserItems");
            DropIndex("dbo.Bids", new[] { "UserItemId" });
            DropIndex("dbo.Bids", new[] { "BidderId" });
            DropIndex("dbo.UserItems", new[] { "HighestBidderId" });
            DropIndex("dbo.UserItems", new[] { "TheaterId" });
            DropIndex("dbo.UserItems", new[] { "ISofAUserId" });
            AlterColumn("dbo.Items", "BoughtDate", c => c.DateTime(precision: 0, storeType: "datetime2"));
            DropTable("dbo.Bids");
            DropTable("dbo.UserItems");
            AddForeignKey("dbo.Items", "TheaterId", "dbo.Theaters", "TheaterId", cascadeDelete: true);
        }
    }
}
