namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentCatalog",
                c => new
                    {
                        PaymentCatalogID = c.Int(nullable: false, identity: true),
                        PaymentCatalogCode = c.String(),
                        Description = c.String(),
                        Type = c.String(),
                        Category = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 8),
                        UsingStatus = c.Boolean(nullable: false),
                        ApproveStatus = c.Boolean(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        ExpiredDate = c.DateTime(),
                        Version = c.Double(nullable: false),
                        Order = c.Int(nullable: false),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentCatalog", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCatalog", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.PaymentCatalog", "CreatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.PaymentCatalog", new[] { "DeletedUserID" });
            DropIndex("dbo.PaymentCatalog", new[] { "UpdatedUserID" });
            DropIndex("dbo.PaymentCatalog", new[] { "CreatedUserID" });
            DropTable("dbo.PaymentCatalog");
        }
    }
}
