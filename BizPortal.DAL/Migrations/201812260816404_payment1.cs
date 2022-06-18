namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentCatalog", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCatalog", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCatalog", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCostCenter", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCostCenter", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCostCenter", "PaymentCatalogID", "dbo.PaymentCatalog");
            DropForeignKey("dbo.PaymentCostCenter", "UpdatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.PaymentCatalog", new[] { "CreatedUserID" });
            DropIndex("dbo.PaymentCatalog", new[] { "UpdatedUserID" });
            DropIndex("dbo.PaymentCatalog", new[] { "DeletedUserID" });
            DropIndex("dbo.PaymentCostCenter", new[] { "PaymentCatalogID" });
            DropIndex("dbo.PaymentCostCenter", new[] { "CreatedUserID" });
            DropIndex("dbo.PaymentCostCenter", new[] { "UpdatedUserID" });
            DropIndex("dbo.PaymentCostCenter", new[] { "DeletedUserID" });
            CreateTable(
                "dbo.PaymentCenter",
                c => new
                    {
                        PaymentCenterID = c.Int(nullable: false, identity: true),
                        PaymentCenterCode = c.String(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Description = c.String(nullable: false),
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
                .PrimaryKey(t => t.PaymentCenterID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            CreateTable(
                "dbo.PaymentHomeCostCenter",
                c => new
                    {
                        PaymentHomeCostCenterID = c.Int(nullable: false, identity: true),
                        CostCenterCode = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        PaymentOrgCode = c.String(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Flag = c.String(),
                        ProvinceCode = c.String(nullable: false),
                        PaymentCenterID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.PaymentHomeCostCenterID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.PaymentCenter", t => t.PaymentCenterID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.PaymentCenterID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
            DropTable("dbo.PaymentCatalog");
            DropTable("dbo.PaymentCostCenter");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.PaymentCostCenterID);
            
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
                .PrimaryKey(t => t.PaymentCatalogID);
            
            DropForeignKey("dbo.PaymentHomeCostCenter", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentHomeCostCenter", "PaymentCenterID", "dbo.PaymentCenter");
            DropForeignKey("dbo.PaymentHomeCostCenter", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentHomeCostCenter", "CreatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCenter", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCenter", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCenter", "CreatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.PaymentHomeCostCenter", new[] { "DeletedUserID" });
            DropIndex("dbo.PaymentHomeCostCenter", new[] { "UpdatedUserID" });
            DropIndex("dbo.PaymentHomeCostCenter", new[] { "CreatedUserID" });
            DropIndex("dbo.PaymentHomeCostCenter", new[] { "PaymentCenterID" });
            DropIndex("dbo.PaymentCenter", new[] { "DeletedUserID" });
            DropIndex("dbo.PaymentCenter", new[] { "UpdatedUserID" });
            DropIndex("dbo.PaymentCenter", new[] { "CreatedUserID" });
            DropTable("dbo.PaymentHomeCostCenter");
            DropTable("dbo.PaymentCenter");
            CreateIndex("dbo.PaymentCostCenter", "DeletedUserID");
            CreateIndex("dbo.PaymentCostCenter", "UpdatedUserID");
            CreateIndex("dbo.PaymentCostCenter", "CreatedUserID");
            CreateIndex("dbo.PaymentCostCenter", "PaymentCatalogID");
            CreateIndex("dbo.PaymentCatalog", "DeletedUserID");
            CreateIndex("dbo.PaymentCatalog", "UpdatedUserID");
            CreateIndex("dbo.PaymentCatalog", "CreatedUserID");
            AddForeignKey("dbo.PaymentCostCenter", "UpdatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PaymentCostCenter", "PaymentCatalogID", "dbo.PaymentCatalog", "PaymentCatalogID");
            AddForeignKey("dbo.PaymentCostCenter", "DeletedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PaymentCostCenter", "CreatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PaymentCatalog", "UpdatedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PaymentCatalog", "DeletedUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.PaymentCatalog", "CreatedUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
