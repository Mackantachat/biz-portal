namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileUpload : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileUpload",
                c => new
                    {
                        FileUploadID = c.Int(nullable: false, identity: true),
                        FileSysName = c.String(nullable: false, maxLength: 512),
                        FileName = c.String(nullable: false, maxLength: 512),
                        AbsolutePath = c.String(nullable: false, maxLength: 1024),
                        ContentType = c.String(nullable: false, maxLength: 512),
                        ContentLength = c.Long(nullable: false),
                        TemporaryStatus = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedUserID = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedUserID = c.String(maxLength: 128),
                        UpdatedDate = c.DateTime(),
                        DeletedUserID = c.String(maxLength: 128),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.FileUploadID)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedUserID)
                .Index(t => t.CreatedUserID)
                .Index(t => t.UpdatedUserID)
                .Index(t => t.DeletedUserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileUpload", "UpdatedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FileUpload", "DeletedUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.FileUpload", "CreatedUserID", "dbo.AspNetUsers");
            DropIndex("dbo.FileUpload", new[] { "DeletedUserID" });
            DropIndex("dbo.FileUpload", new[] { "UpdatedUserID" });
            DropIndex("dbo.FileUpload", new[] { "CreatedUserID" });
            DropTable("dbo.FileUpload");
        }
    }
}
