namespace Crawler.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAbbreviation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Boards", "Abbreviation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Boards", "Abbreviation");
        }
    }
}
