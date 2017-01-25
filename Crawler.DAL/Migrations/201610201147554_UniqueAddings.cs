namespace Crawler.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UniqueAddings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Boards", "Url", c => c.String(maxLength: 255));
            AlterColumn("dbo.BoardThemes", "Url", c => c.String(maxLength: 255));
            AlterColumn("dbo.BoardThreads", "Url", c => c.String(maxLength: 255));
            CreateIndex("dbo.Boards", "Url", unique: true);
            CreateIndex("dbo.BoardThemes", "Url", unique: true);
            CreateIndex("dbo.BoardThreads", "Url", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.BoardThreads", new[] { "Url" });
            DropIndex("dbo.BoardThemes", new[] { "Url" });
            DropIndex("dbo.Boards", new[] { "Url" });
            AlterColumn("dbo.BoardThreads", "Url", c => c.String());
            AlterColumn("dbo.BoardThemes", "Url", c => c.String());
            AlterColumn("dbo.Boards", "Url", c => c.String());
        }
    }
}
