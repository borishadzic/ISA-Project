namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullable_Seat_UserId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Seats", new[] { "UserId" });
            AlterColumn("dbo.Seats", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Seats", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Seats", new[] { "UserId" });
            AlterColumn("dbo.Seats", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Seats", "UserId");
        }
    }
}
