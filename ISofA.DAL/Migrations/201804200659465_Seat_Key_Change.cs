namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Seat_Key_Change : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Seats");
            AddPrimaryKey("dbo.Seats", new[] { "ProjectionId", "SeatRow", "SeatColumn" });
            CreateIndex("dbo.Seats", "ProjectionId");
            CreateIndex("dbo.Seats", "PlayId");
            CreateIndex("dbo.Seats", "StageId");
            AddForeignKey("dbo.Seats", "PlayId", "dbo.Plays", "PlayId", cascadeDelete: true);
            AddForeignKey("dbo.Seats", "StageId", "dbo.Stages", "StageId", cascadeDelete: true);
            AddForeignKey("dbo.Seats", "ProjectionId", "dbo.Projections", "ProjectionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seats", "ProjectionId", "dbo.Projections");
            DropForeignKey("dbo.Seats", "StageId", "dbo.Stages");
            DropForeignKey("dbo.Seats", "PlayId", "dbo.Plays");
            DropIndex("dbo.Seats", new[] { "StageId" });
            DropIndex("dbo.Seats", new[] { "PlayId" });
            DropIndex("dbo.Seats", new[] { "ProjectionId" });
            DropPrimaryKey("dbo.Seats");
            AddPrimaryKey("dbo.Seats", new[] { "TheaterId", "SeatRow", "SeatColumn" });
        }
    }
}
