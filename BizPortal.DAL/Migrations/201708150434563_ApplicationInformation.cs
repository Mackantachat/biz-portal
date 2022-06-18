namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationInformation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Application", "HandbookUrl", c => c.String());
            AddColumn("dbo.Application", "LogoFileID", c => c.Int());
            AddColumn("dbo.Application", "OperatingDays", c => c.Int());
            AddColumn("dbo.Application", "OperatingCost", c => c.Decimal(precision: 18, scale: 8));
            CreateIndex("dbo.Application", "LogoFileID");
            AddForeignKey("dbo.Application", "LogoFileID", "dbo.FileUpload", "FileUploadID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Application", "LogoFileID", "dbo.FileUpload");
            DropIndex("dbo.Application", new[] { "LogoFileID" });
            DropColumn("dbo.Application", "OperatingCost");
            DropColumn("dbo.Application", "OperatingDays");
            DropColumn("dbo.Application", "LogoFileID");
            DropColumn("dbo.Application", "HandbookUrl");
        }
    }
}
