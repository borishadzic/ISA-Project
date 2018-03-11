namespace ISofA.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ListAndRemoveFriends : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FriendRequests", "Accepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FriendRequests", "Accepted");
        }
    }
}
