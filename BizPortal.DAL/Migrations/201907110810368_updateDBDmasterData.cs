namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDBDmasterData : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DBDCommerceOffice");
            AddColumn("dbo.SerivceCategoryList", "StructureData", c => c.String(nullable: false));
            AlterColumn("dbo.DBDCommerceOffice", "ProvinceCode", c => c.String(nullable: false, maxLength: 2));
            AddPrimaryKey("dbo.DBDCommerceOffice", new[] { "Code", "ProvinceCode" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DBDCommerceOffice");
            AlterColumn("dbo.DBDCommerceOffice", "ProvinceCode", c => c.String(maxLength: 2));
            DropColumn("dbo.SerivceCategoryList", "StructureData");
            AddPrimaryKey("dbo.DBDCommerceOffice", "Code");
        }
    }
}
