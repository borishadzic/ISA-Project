namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FriendRequestSystem : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Projections", new[] { "TheaterId" });
            CreateTable(
                "dbo.FriendRequests",
                c => new
                    {
                        SenderId = c.String(nullable: false, maxLength: 128),
                        RecieverId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.SenderId, t.RecieverId })
                .ForeignKey("dbo.AspNetUsers", t => t.RecieverId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecieverId);
            
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "ISofAUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "ISofAUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ISofAUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            DropForeignKey("dbo.FriendRequests", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "RecieverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ISofAUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ISofAUser_Id" });
            DropIndex("dbo.FriendRequests", new[] { "RecieverId" });
            DropIndex("dbo.FriendRequests", new[] { "SenderId" });
            DropColumn("dbo.AspNetUsers", "ISofAUser_Id");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "Name");
            DropTable("dbo.FriendRequests");
            CreateIndex("dbo.Projections", "TheaterId");
        }
    }
}
