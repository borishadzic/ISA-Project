namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Friends : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "ISofAUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "ISofAUser_Id" });
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
            
            DropColumn("dbo.AspNetUsers", "ISofAUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ISofAUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Friends", "FriendId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "FriendId" });
            DropIndex("dbo.Friends", new[] { "Id" });
            DropTable("dbo.Friends");
            CreateIndex("dbo.AspNetUsers", "ISofAUser_Id");
            AddForeignKey("dbo.AspNetUsers", "ISofAUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
