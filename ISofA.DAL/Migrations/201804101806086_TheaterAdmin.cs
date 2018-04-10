namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheaterAdmin : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "TheaterId", newName: "AdminOfTheaterId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_TheaterId", newName: "IX_AdminOfTheaterId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AdminOfTheaterId", newName: "IX_TheaterId");
            RenameColumn(table: "dbo.AspNetUsers", name: "AdminOfTheaterId", newName: "TheaterId");
        }
    }
}
