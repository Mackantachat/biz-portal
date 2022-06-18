namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterApplication1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "TemporaryRemark", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "TemporaryRemark");
        }
    }
}
