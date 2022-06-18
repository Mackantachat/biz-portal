namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationStatusSequence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "StatusSequence", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "StatusSequence");
        }
    }
}
