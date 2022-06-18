namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPaymentExtendData : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.PaymentExtendDataID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PaymentExtendData", "ApplicationID", "dbo.Application");
            DropIndex("dbo.PaymentExtendData", new[] { "ApplicationID" });
            DropTable("dbo.PaymentExtendData");
        }
    }
}
