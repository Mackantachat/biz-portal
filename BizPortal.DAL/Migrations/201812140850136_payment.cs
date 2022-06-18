namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentCatalog",
                c => new
                    {
                        PaymentCatalogID = c.Int(nullable: false, identity: true),
                        CatalogCode = c.String(nullable: false),
                        ShortCatalogDesc = c.String(nullable: false),
                        CatalogDesc = c.String(nullable: false),
                        Flag = c.String(),
                        LegalClosingDate = c.DateTime(),
                        AccountClosingDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentCatalogID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.PaymentCostCenter",
                c => new
                    {
                        PaymentCostCenterID = c.Int(nullable: false, identity: true),
                        CostCenterCode = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        PaymentOrgCode = c.String(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Flag = c.String(),
                        ProvinceCode = c.String(nullable: false),
                        PaymentCatalogID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentCostCenterID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.PaymentCatalog", t => t.PaymentCatalogID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.PaymentCatalogID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentCostCenter", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCostCenter", "PaymentCatalogID", "dbo.PaymentCatalog");
            DropForeignKey("dbo.PaymentCostCenter", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCostCenter", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCatalog", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCatalog", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCatalog", "CreatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.PaymentCostCenter", new[] { "DeletedUserID" });
            DropIndex("dbo.PaymentCostCenter", new[] { "UpdatedUserID" });
            DropIndex("dbo.PaymentCostCenter", new[] { "CreatedUserID" });
            DropIndex("dbo.PaymentCostCenter", new[] { "PaymentCatalogID" });
            DropIndex("dbo.PaymentCatalog", new[] { "DeletedUserID" });
            DropIndex("dbo.PaymentCatalog", new[] { "UpdatedUserID" });
            DropIndex("dbo.PaymentCatalog", new[] { "CreatedUserID" });
            DropTable("dbo.PaymentCostCenter");
            DropTable("dbo.PaymentCatalog");
        }
    }
}
