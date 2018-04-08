namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bids",
                c => new
                    {
                        UserItemId = c.Guid(nullable: false),
                        BidderId = c.String(nullable: false, maxLength: 128),
                        BidDate = c.DateTime(nullable: false),
                        BidAmount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserItemId, t.BidderId })
                .ForeignKey("dbo.UserItems", t => t.UserItemId)
                .ForeignKey("dbo.AspNetUsers", t => t.BidderId)
                .Index(t => t.UserItemId)
                .Index(t => t.BidderId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        City = c.String(),
                        ISofAUserRole = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        AdminOfTheater_TheaterId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Theaters", t => t.AdminOfTheater_TheaterId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.AdminOfTheater_TheaterId);
            
            CreateTable(
                "dbo.Theaters",
                c => new
                    {
                        TheaterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Single(nullable: false),
                        Longitude = c.Single(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TheaterId);
            
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
                        BoughtDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyerId)
                .Index(t => t.TheaterId)
                .Index(t => t.BuyerId);
            
            CreateTable(
                "dbo.Plays",
                c => new
                    {
                        PlayId = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(),
                        Actors = c.String(),
                        Genre = c.String(),
                        Director = c.String(),
                        DurationMins = c.Int(nullable: false),
                        PosterUrl = c.String(),
                        TrailerUrl = c.String(),
                        Description = c.String(),
                        TheaterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .Index(t => t.TheaterId);
            
            CreateTable(
                "dbo.Projections",
                c => new
                    {
                        ProjectionId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        Price = c.Int(nullable: false),
                        PlayId = c.Int(nullable: false),
                        StageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectionId)
                .ForeignKey("dbo.Stages", t => t.StageId)
                .ForeignKey("dbo.Plays", t => t.PlayId)
                .Index(t => t.PlayId)
                .Index(t => t.StageId);
            
            CreateTable(
                "dbo.Stages",
                c => new
                    {
                        StageId = c.Int(nullable: false, identity: true),
                        SeatRows = c.Int(nullable: false),
                        SeatColumns = c.Int(nullable: false),
                        TheaterId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StageId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .Index(t => t.TheaterId);
            
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
                        Sold = c.Boolean(nullable: false),
                        HighestBid = c.Single(),
                    })
                .PrimaryKey(t => t.UserItemId)
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .ForeignKey("dbo.AspNetUsers", t => t.ISofAUserId)
                .Index(t => t.ISofAUserId)
                .Index(t => t.TheaterId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        SenderId = c.String(nullable: false, maxLength: 128),
                        RecieverId = c.String(nullable: false, maxLength: 128),
                        Accepted = c.Boolean(),
                    })
                .PrimaryKey(t => new { t.SenderId, t.RecieverId })
                .ForeignKey("dbo.AspNetUsers", t => t.RecieverId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecieverId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        TheaterId = c.Int(nullable: false),
                        SeatRow = c.Int(nullable: false),
                        SeatColumn = c.Int(nullable: false),
                        PlayId = c.Int(nullable: false),
                        StageId = c.Int(nullable: false),
                        ProjectionId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        State = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TheaterId, t.SeatRow, t.SeatColumn })
                .ForeignKey("dbo.Theaters", t => t.TheaterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TheaterId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.FanZoneAdmins",
                c => new
                    {
                        TheaterId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TheaterId, t.UserId })
                .ForeignKey("dbo.Theaters", t => t.TheaterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TheaterId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TheaterAdmins",
                c => new
                    {
                        TheaterId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TheaterId, t.UserId })
                .ForeignKey("dbo.Theaters", t => t.TheaterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.TheaterId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FriendId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Id, t.FriendId })
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FriendId)
                .Index(t => t.Id)
                .Index(t => t.FriendId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserItems", "ISofAUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bids", "BidderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Seats", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Seats", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "RecieverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Items", "BuyerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserItems", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Bids", "UserItemId", "dbo.UserItems");
            DropForeignKey("dbo.TheaterAdmins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TheaterAdmins", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Stages", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Plays", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Projections", "PlayId", "dbo.Plays");
            DropForeignKey("dbo.Projections", "StageId", "dbo.Stages");
            DropForeignKey("dbo.Items", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.FanZoneAdmins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FanZoneAdmins", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.AspNetUsers", "AdminOfTheater_TheaterId", "dbo.Theaters");
            DropIndex("dbo.Friends", new[] { "FriendId" });
            DropIndex("dbo.Friends", new[] { "Id" });
            DropIndex("dbo.TheaterAdmins", new[] { "UserId" });
            DropIndex("dbo.TheaterAdmins", new[] { "TheaterId" });
            DropIndex("dbo.FanZoneAdmins", new[] { "UserId" });
            DropIndex("dbo.FanZoneAdmins", new[] { "TheaterId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Seats", new[] { "UserId" });
            DropIndex("dbo.Seats", new[] { "TheaterId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.FriendRequests", new[] { "RecieverId" });
            DropIndex("dbo.FriendRequests", new[] { "SenderId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.UserItems", new[] { "TheaterId" });
            DropIndex("dbo.UserItems", new[] { "ISofAUserId" });
            DropIndex("dbo.Stages", new[] { "TheaterId" });
            DropIndex("dbo.Projections", new[] { "StageId" });
            DropIndex("dbo.Projections", new[] { "PlayId" });
            DropIndex("dbo.Plays", new[] { "TheaterId" });
            DropIndex("dbo.Items", new[] { "BuyerId" });
            DropIndex("dbo.Items", new[] { "TheaterId" });
            DropIndex("dbo.AspNetUsers", new[] { "AdminOfTheater_TheaterId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Bids", new[] { "BidderId" });
            DropIndex("dbo.Bids", new[] { "UserItemId" });
            DropTable("dbo.Friends");
            DropTable("dbo.TheaterAdmins");
            DropTable("dbo.FanZoneAdmins");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Seats");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.FriendRequests");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.UserItems");
            DropTable("dbo.Stages");
            DropTable("dbo.Projections");
            DropTable("dbo.Plays");
            DropTable("dbo.Items");
            DropTable("dbo.Theaters");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Bids");
        }
    }
}
