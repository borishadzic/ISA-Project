namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.Items", "BoughtDate", c => c.DateTime(precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "BoughtDate");
            DropColumn("dbo.Items", "Price");
        }
    }
}
