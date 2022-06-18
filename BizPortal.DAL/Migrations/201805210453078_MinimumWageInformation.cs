namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MinimumWageInformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SSOMinimumWage",
                c => new
                    {
                        SSOMinimumWageID = c.Int(nullable: false, identity: true),
                        ProvinceCode = c.String(),
                        MinimumWage = c.Decimal(nullable: false, precision: 18, scale: 8),
                    })
                .PrimaryKey(t => t.SSOMinimumWageID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SSOMinimumWage");
        }
    }
}
