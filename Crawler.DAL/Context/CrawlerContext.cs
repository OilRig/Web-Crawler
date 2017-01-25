using Crawler.DAL.Entities;
using System.Data.Entity;

namespace Crawler.DAL
{
    public class CrawlerContext : DbContext
    {
        public CrawlerContext() : base("CrawlingData")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CrawlerContext, Migrations.Configuration>("CrawlingData"));
            //Database.SetInitializer(new DropCreateDatabaseAlways<CrawlerContext>());
        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardTheme> BoardThemes { get; set; }
        public DbSet<BoardThread> BoardThreads { get; set; }
        public DbSet<ThreadPost> ThreadPosts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
