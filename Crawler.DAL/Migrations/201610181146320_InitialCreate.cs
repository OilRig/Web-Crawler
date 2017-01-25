namespace Crawler.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Boards",
                c => new
                    {
                        BoardId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.BoardId);
            
            CreateTable(
                "dbo.BoardThemes",
                c => new
                    {
                        ThemeId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Url = c.String(),
                        BoardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThemeId)
                .ForeignKey("dbo.Boards", t => t.BoardId, cascadeDelete: true)
                .Index(t => t.BoardId);
            
            CreateTable(
                "dbo.BoardThreads",
                c => new
                    {
                        ThreadId = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Url = c.String(),
                        ThemeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThreadId)
                .ForeignKey("dbo.BoardThemes", t => t.ThemeId, cascadeDelete: true)
                .Index(t => t.ThemeId);
            
            CreateTable(
                "dbo.ThreadPosts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        PostContent = c.String(),
                        UserId = c.Int(nullable: false),
                        ThreadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.BoardThreads", t => t.ThreadId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ThreadId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ThreadPosts", "UserId", "dbo.Users");
            DropForeignKey("dbo.ThreadPosts", "ThreadId", "dbo.BoardThreads");
            DropForeignKey("dbo.BoardThreads", "ThemeId", "dbo.BoardThemes");
            DropForeignKey("dbo.BoardThemes", "BoardId", "dbo.Boards");
            DropIndex("dbo.ThreadPosts", new[] { "ThreadId" });
            DropIndex("dbo.ThreadPosts", new[] { "UserId" });
            DropIndex("dbo.BoardThreads", new[] { "ThemeId" });
            DropIndex("dbo.BoardThemes", new[] { "BoardId" });
            DropTable("dbo.Users");
            DropTable("dbo.ThreadPosts");
            DropTable("dbo.BoardThreads");
            DropTable("dbo.BoardThemes");
            DropTable("dbo.Boards");
        }
    }
}
