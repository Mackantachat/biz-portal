namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThumbnailFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "ThumbnailID", c => c.Int());
            AddColumn("dbo.Category", "ThumbnailID", c => c.Int());
            AddColumn("dbo.Section", "ThumbnailID", c => c.Int());
            CreateIndex("dbo.Article", "ThumbnailID");
            CreateIndex("dbo.Category", "ThumbnailID");
            CreateIndex("dbo.Section", "ThumbnailID");
            AddForeignKey("dbo.Section", "ThumbnailID", "dbo.FileUpload", "FileUploadID");
            AddForeignKey("dbo.Category", "ThumbnailID", "dbo.FileUpload", "FileUploadID");
            AddForeignKey("dbo.Article", "ThumbnailID", "dbo.FileUpload", "FileUploadID");
            DropColumn("dbo.Article", "ThumbnailUrl");
            DropColumn("dbo.Category", "ThumbnailUrl");
            DropColumn("dbo.Section", "ThumbnailUrl");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Section", "ThumbnailUrl", c => c.String(maxLength: 2000));
            AddColumn("dbo.Category", "ThumbnailUrl", c => c.String(maxLength: 2000));
            AddColumn("dbo.Article", "ThumbnailUrl", c => c.String(maxLength: 2000));
            DropForeignKey("dbo.Article", "ThumbnailID", "dbo.FileUpload");
            DropForeignKey("dbo.Category", "ThumbnailID", "dbo.FileUpload");
            DropForeignKey("dbo.Section", "ThumbnailID", "dbo.FileUpload");
            DropIndex("dbo.Section", new[] { "ThumbnailID" });
            DropIndex("dbo.Category", new[] { "ThumbnailID" });
            DropIndex("dbo.Article", new[] { "ThumbnailID" });
            DropColumn("dbo.Section", "ThumbnailID");
            DropColumn("dbo.Category", "ThumbnailID");
            DropColumn("dbo.Article", "ThumbnailID");
        }
    }
}
