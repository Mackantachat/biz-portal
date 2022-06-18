namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTransactionReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PermitSummaryReport",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IdentityType = c.String(nullable: false, maxLength: 30),
                        ApplicationID = c.Int(nullable: false),
                        OrgNameTH = c.String(),
                        OrgCode = c.String(nullable: false, maxLength: 20),
                        PermitName = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        Status = c.String(nullable: false),
                        StatusOther = c.String(nullable: false),
                        Fee = c.Decimal(precision: 18, scale: 8),
                        EMSFee = c.Decimal(precision: 18, scale: 8),
                        IdentityID = c.String(nullable: false, maxLength: 13),
                        IdentityName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        ExpectedFinishDate = c.DateTime(),
                        ProvinceID = c.Int(nullable: false),
                        Province = c.String(),
                        AmphurID = c.Int(nullable: false),
                        Amphur = c.String(),
                        TumbolID = c.Int(nullable: false),
                        Tumbol = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PermitSummaryReport");
        }
    }
}
