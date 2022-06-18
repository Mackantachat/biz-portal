namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TSICinformation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TSICcode", "TSICMultiple", c => c.Decimal(nullable: false, precision: 18, scale: 8));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TSICcode", "TSICMultiple");
        }
    }
}
