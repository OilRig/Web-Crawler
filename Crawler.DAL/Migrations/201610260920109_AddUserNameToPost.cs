namespace Crawler.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserNameToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ThreadPosts", "AuthorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ThreadPosts", "AuthorName");
        }
    }
}
