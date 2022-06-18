namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSigning : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FormExtendData", "ApplicationID", "dbo.Application");
            DropIndex("dbo.FormExtendData", new[] { "ApplicationID" });
            CreateTable(
                "dbo.SigningExtendedData",
                c => new
                    {
                        SigningExtendedDataID = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        UserType = c.Int(nullable: false),
                        Name = c.String(),
                        Label = c.String(),
                        Value = c.String(),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SigningExtendedDataID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
            AddColumn("dbo.Application", "SigningDocumentCitizenTemplateID", c => c.String());
            AddColumn("dbo.Application", "SigningDocumentJuristicTemplateID", c => c.String());
            DropColumn("dbo.Application", "SigningDocumentTemplateID");
            DropTable("dbo.FormExtendData");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.FormExtendDataID);
            
            AddColumn("dbo.Application", "SigningDocumentTemplateID", c => c.String());
            DropForeignKey("dbo.SigningExtendedData", "ApplicationID", "dbo.Application");
            DropIndex("dbo.SigningExtendedData", new[] { "ApplicationID" });
            DropColumn("dbo.Application", "SigningDocumentJuristicTemplateID");
            DropColumn("dbo.Application", "SigningDocumentCitizenTemplateID");
            DropTable("dbo.SigningExtendedData");
            CreateIndex("dbo.FormExtendData", "ApplicationID");
            AddForeignKey("dbo.FormExtendData", "ApplicationID", "dbo.Application", "ApplicationID");
        }
    }
}
