namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateApplicationSigning : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SigningPosition",
                c => new
                    {
                        SigningPositionID = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Left = c.String(),
                        Bottom = c.String(),
                        Width = c.String(),
                        Height = c.String(),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SigningPositionID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
            DropColumn("dbo.SigningPerson", "Left");
            DropColumn("dbo.SigningPerson", "Bottom");
            DropColumn("dbo.SigningPerson", "Width");
            DropColumn("dbo.SigningPerson", "Height");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SigningPerson", "Height", c => c.String());
            AddColumn("dbo.SigningPerson", "Width", c => c.String());
            AddColumn("dbo.SigningPerson", "Bottom", c => c.String());
            AddColumn("dbo.SigningPerson", "Left", c => c.String());
            DropForeignKey("dbo.SigningPosition", "ApplicationID", "dbo.Application");
            DropIndex("dbo.SigningPosition", new[] { "ApplicationID" });
            DropTable("dbo.SigningPosition");
        }
    }
}
