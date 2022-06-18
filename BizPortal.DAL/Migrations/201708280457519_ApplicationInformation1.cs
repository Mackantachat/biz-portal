namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationInformation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "CitizenApplicationUrl", c => c.String());
            AddColumn("dbo.Application", "CitizenHandbookUrl", c => c.String());
            AddColumn("dbo.Application", "CitizenOperatingDays", c => c.Int());
            AddColumn("dbo.Application", "CitizenOperatingCostType", c => c.String());
            AddColumn("dbo.Application", "CitizenOperatingCost", c => c.Decimal(precision: 18, scale: 8));
            AddColumn("dbo.Application", "CitizenOperatingCost2", c => c.Decimal(precision: 18, scale: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "CitizenOperatingCost2");
            DropColumn("dbo.Application", "CitizenOperatingCost");
            DropColumn("dbo.Application", "CitizenOperatingCostType");
            DropColumn("dbo.Application", "CitizenOperatingDays");
            DropColumn("dbo.Application", "CitizenHandbookUrl");
            DropColumn("dbo.Application", "CitizenApplicationUrl");
        }
    }
}
