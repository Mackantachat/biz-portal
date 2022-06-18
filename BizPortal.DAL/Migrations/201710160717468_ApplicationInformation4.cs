namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationInformation4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "OperatingDayType", c => c.String());
            AddColumn("dbo.Application", "OperatingDays2", c => c.Int());
            AddColumn("dbo.Application", "CitizenOperatingDayType", c => c.String());
            AddColumn("dbo.Application", "CitizenOperatingDays2", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "CitizenOperatingDays2");
            DropColumn("dbo.Application", "CitizenOperatingDayType");
            DropColumn("dbo.Application", "OperatingDays2");
            DropColumn("dbo.Application", "OperatingDayType");
        }
    }
}
