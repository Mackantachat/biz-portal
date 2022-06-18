namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Business", "ApplicationID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Business", "ApplicationID");
        }
    }
}
