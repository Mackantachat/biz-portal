namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterApplication : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "TemporaryDisable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "TemporaryDisable");
        }
    }
}
