namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Application", "StatusSequence");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Application", "StatusSequence", c => c.String());
        }
    }
}
