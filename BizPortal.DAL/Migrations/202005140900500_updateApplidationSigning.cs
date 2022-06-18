namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateApplidationSigning : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SigningPerson",
                c => new
                    {
                        SigningPersonID = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        CitizenID = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Position = c.String(),
                        SignatureBase64 = c.String(),
                        Left = c.String(),
                        Bottom = c.String(),
                        Width = c.String(),
                        Height = c.String(),
                        Application_ApplicationID = c.Int(),
                    })
                .PrimaryKey(t => t.SigningPersonID)
                .ForeignKey("dbo.Application", t => t.Application_ApplicationID)
                .Index(t => t.Application_ApplicationID);
            
            AddColumn("dbo.Application", "IsEnableSigning", c => c.Boolean(nullable: false));
            AddColumn("dbo.Application", "SigningType", c => c.String());
            AddColumn("dbo.Application", "SigningDocumentType", c => c.String());
            AddColumn("dbo.Application", "SigningDocumentTemplateID", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SigningPerson", "Application_ApplicationID", "dbo.Application");
            DropIndex("dbo.SigningPerson", new[] { "Application_ApplicationID" });
            DropColumn("dbo.Application", "SigningDocumentTemplateID");
            DropColumn("dbo.Application", "SigningDocumentType");
            DropColumn("dbo.Application", "SigningType");
            DropColumn("dbo.Application", "IsEnableSigning");
            DropTable("dbo.SigningPerson");
        }
    }
}
