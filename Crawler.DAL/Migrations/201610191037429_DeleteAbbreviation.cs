namespace Crawler.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteAbbreviation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Boards", "Abbreviation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Boards", "Abbreviation", c => c.String());
        }
    }
}
