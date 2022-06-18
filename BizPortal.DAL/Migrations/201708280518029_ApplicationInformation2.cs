namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationInformation2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "OperatingCostType", c => c.String());
            AddColumn("dbo.Application", "OperatingCost2", c => c.Decimal(precision: 18, scale: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Application", "OperatingCost2");
            DropColumn("dbo.Application", "OperatingCostType");
        }
    }
}
