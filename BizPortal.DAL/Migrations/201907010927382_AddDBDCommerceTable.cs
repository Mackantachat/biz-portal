namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDBDCommerceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DBDCommerceObjective",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 5),
                        DescriptionTh = c.String(),
                        ActiveFlag = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.DBDCommerceOffice",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 5),
                        OfficeNameTh = c.String(),
                        OfficeType_description = c.String(),
                        OfficeType_descriptionShort = c.String(),
                        OfficeCode = c.String(maxLength: 3),
                        AmphurCode = c.String(maxLength: 2),
                        ProvinceCode = c.String(maxLength: 2),
                        ActiveFlag = c.String(maxLength: 1),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DBDCommerceOffice");
            DropTable("dbo.DBDCommerceObjective");
        }
    }
}
