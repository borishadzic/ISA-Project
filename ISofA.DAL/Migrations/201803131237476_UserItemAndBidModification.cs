namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserItemAndBidModification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserItems", "HighestBidderId", "dbo.AspNetUsers");
            DropIndex("dbo.UserItems", new[] { "HighestBidderId" });
            DropPrimaryKey("dbo.Bids");
            AlterColumn("dbo.UserItems", "Sold", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.Bids", new[] { "UserItemId", "BidderId" });
            DropColumn("dbo.UserItems", "HighestBidderId");
            DropColumn("dbo.Bids", "BidId");
            DropColumn("dbo.Bids", "Won");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bids", "Won", c => c.Boolean(nullable: false));
            AddColumn("dbo.Bids", "BidId", c => c.Guid(nullable: false, identity: true));
            AddColumn("dbo.UserItems", "HighestBidderId", c => c.String(maxLength: 128));
            DropPrimaryKey("dbo.Bids");
            AlterColumn("dbo.UserItems", "Sold", c => c.Boolean());
            AddPrimaryKey("dbo.Bids", "BidId");
            CreateIndex("dbo.UserItems", "HighestBidderId");
            AddForeignKey("dbo.UserItems", "HighestBidderId", "dbo.AspNetUsers", "Id");
        }
    }
}
