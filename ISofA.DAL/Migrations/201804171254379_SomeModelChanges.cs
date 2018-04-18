namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeModelChanges : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "AdminOfTheater_TheaterId", newName: "AdminOfTheaterId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AdminOfTheater_TheaterId", newName: "IX_AdminOfTheaterId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_AdminOfTheaterId", newName: "IX_AdminOfTheater_TheaterId");
            RenameColumn(table: "dbo.AspNetUsers", name: "AdminOfTheaterId", newName: "AdminOfTheater_TheaterId");
        }
    }
}
