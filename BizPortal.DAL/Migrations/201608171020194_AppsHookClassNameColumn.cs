namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppsHookClassNameColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "AppsHookClassName", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "AppsHookClassName");
        }
    }
}
