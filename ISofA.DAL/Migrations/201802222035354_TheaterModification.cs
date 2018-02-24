namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheaterModification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Theaters", "Latitude", c => c.Single(nullable: false));
            AddColumn("dbo.Theaters", "Longitude", c => c.Single(nullable: false));
            AddColumn("dbo.Theaters", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Theaters", "Type");
            DropColumn("dbo.Theaters", "Longitude");
            DropColumn("dbo.Theaters", "Latitude");
        }
    }
}
