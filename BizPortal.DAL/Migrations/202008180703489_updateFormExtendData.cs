namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFormExtendData : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentExtendData", "ApplicationID", "dbo.Application");
            DropIndex("dbo.PaymentExtendData", new[] { "ApplicationID" });
            CreateTable(
                "dbo.FormExtendData",
                c => new
                    {
                        FormExtendDataID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Name = c.String(),
                        Label = c.String(),
                        Value = c.String(),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FormExtendDataID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
            DropTable("dbo.PaymentExtendData");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentExtendData",
                c => new
                    {
                        PaymentExtendDataID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Type = c.Int(nullable: false),
                        Value = c.String(),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentExtendDataID);
            
            DropForeignKey("dbo.FormExtendData", "ApplicationID", "dbo.Application");
            DropIndex("dbo.FormExtendData", new[] { "ApplicationID" });
            DropTable("dbo.FormExtendData");
            CreateIndex("dbo.PaymentExtendData", "ApplicationID");
            AddForeignKey("dbo.PaymentExtendData", "ApplicationID", "dbo.Application", "ApplicationID");
        }
    }
}
