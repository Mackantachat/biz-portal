namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinimumWageInformation1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SSOMinimumWage", "ProvinceCode", c => c.String(maxLength: 2));
            CreateIndex("dbo.SSOMinimumWage", "ProvinceCode");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SSOMinimumWage", new[] { "ProvinceCode" });
            AlterColumn("dbo.SSOMinimumWage", "ProvinceCode", c => c.String());
        }
    }
}
