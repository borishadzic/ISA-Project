namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plays",
                c => new
                    {
                        TheaterId = c.Int(nullable: false),
                        PlayId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Actors = c.String(),
                        Genre = c.String(),
                        Director = c.String(),
                        DurationMins = c.Int(nullable: false),
                        PosterUrl = c.String(),
                        TrailerUrl = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.TheaterId, t.PlayId })
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .Index(t => t.TheaterId);
            
            CreateTable(
                "dbo.Projections",
                c => new
                    {
                        TheaterId = c.Int(nullable: false),
                        PlayId = c.Int(nullable: false),
                        StageId = c.Int(nullable: false),
                        ProjectionId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TheaterId, t.PlayId, t.StageId, t.ProjectionId })
                .ForeignKey("dbo.Stages", t => new { t.TheaterId, t.StageId })
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .ForeignKey("dbo.Plays", t => new { t.TheaterId, t.PlayId })
                .Index(t => new { t.TheaterId, t.StageId })
                .Index(t => new { t.TheaterId, t.PlayId });
            
            CreateTable(
                "dbo.Seats",
                c => new
                    {
                        TheaterId = c.Int(nullable: false),
                        PlayId = c.Int(nullable: false),
                        StageId = c.Int(nullable: false),
                        ProjectionId = c.Int(nullable: false),
                        SeatRow = c.Int(nullable: false),
                        SeatColumn = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        Discount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TheaterId, t.PlayId, t.StageId, t.ProjectionId, t.SeatRow, t.SeatColumn })
                .ForeignKey("dbo.Projections", t => new { t.TheaterId, t.PlayId, t.StageId, t.ProjectionId }, cascadeDelete: true)
                .Index(t => new { t.TheaterId, t.PlayId, t.StageId, t.ProjectionId });
            
            CreateTable(
                "dbo.Stages",
                c => new
                    {
                        TheaterId = c.Int(nullable: false),
                        StageId = c.Int(nullable: false, identity: true),
                        SeatRows = c.Int(nullable: false),
                        SeatColumns = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TheaterId, t.StageId })
                .ForeignKey("dbo.Theaters", t => t.TheaterId)
                .Index(t => t.TheaterId);
            
            CreateTable(
                "dbo.Theaters",
                c => new
                    {
                        TheaterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TheaterId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Projections", new[] { "TheaterId", "PlayId" }, "dbo.Plays");
            DropForeignKey("dbo.TheaterAdmins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TheaterAdmins", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Stages", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Plays", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.Projections", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.FanZoneAdmins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FanZoneAdmins", "TheaterId", "dbo.Theaters");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Projections", new[] { "TheaterId", "StageId" }, "dbo.Stages");
            DropForeignKey("dbo.Seats", new[] { "TheaterId", "PlayId", "StageId", "ProjectionId" }, "dbo.Projections");
            DropIndex("dbo.TheaterAdmins", new[] { "UserId" });
            DropIndex("dbo.TheaterAdmins", new[] { "TheaterId" });
            DropIndex("dbo.FanZoneAdmins", new[] { "UserId" });
            DropIndex("dbo.FanZoneAdmins", new[] { "TheaterId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Stages", new[] { "TheaterId" });
            DropIndex("dbo.Seats", new[] { "TheaterId", "PlayId", "StageId", "ProjectionId" });
            DropIndex("dbo.Projections", new[] { "TheaterId", "PlayId" });
            DropIndex("dbo.Projections", new[] { "TheaterId", "StageId" });
            DropIndex("dbo.Plays", new[] { "TheaterId" });
            DropTable("dbo.TheaterAdmins");
            DropTable("dbo.FanZoneAdmins");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Theaters");
            DropTable("dbo.Stages");
            DropTable("dbo.Seats");
            DropTable("dbo.Projections");
            DropTable("dbo.Plays");
        }
    }
}
