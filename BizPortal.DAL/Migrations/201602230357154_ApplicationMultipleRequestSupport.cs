namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationMultipleRequestSupport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "MultipleRequestSupport", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "MultipleRequestSupport");
        }
    }
}
