namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SysName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "ArticleSysName", c => c.String(nullable: false, maxLength: 450));
            AddColumn("dbo.Category", "CategorySysName", c => c.String(nullable: false, maxLength: 450));
            AddColumn("dbo.Section", "SectionSysName", c => c.String(nullable: false, maxLength: 450));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Section", "SectionSysName");
            DropColumn("dbo.Category", "CategorySysName");
            DropColumn("dbo.Article", "ArticleSysName");
        }
    }
}
