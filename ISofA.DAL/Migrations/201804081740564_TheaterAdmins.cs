namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheaterAdmins : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Theater_TheaterId", "dbo.Theaters");
            DropIndex("dbo.AspNetUsers", new[] { "Theater_TheaterId" });
            DropColumn("dbo.AspNetUsers", "Theater_TheaterId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Theater_TheaterId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Theater_TheaterId");
            AddForeignKey("dbo.AspNetUsers", "Theater_TheaterId", "dbo.Theaters", "TheaterId");
        }
    }
}
