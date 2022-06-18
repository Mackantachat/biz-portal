namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TSICinformation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TSICcategory",
                c => new
                    {
                        TSICcategoryID = c.Int(nullable: false, identity: true),
                        CategoryCode = c.String(nullable: false, maxLength: 1),
                        CategoryName = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.TSICcategoryID);
            
            CreateTable(
                "dbo.TSICcode",
                c => new
                    {
                        TSICcodeID = c.Int(nullable: false, identity: true),
                        TSICsubGroupID = c.Int(nullable: false),
                        TSICNumber = c.String(nullable: false, maxLength: 5),
                        TSICName = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.TSICcodeID)
                .ForeignKey("dbo.TSICsubGroup", t => t.TSICsubGroupID)
                .Index(t => t.TSICsubGroupID);
            
            CreateTable(
                "dbo.TSICsubGroup",
                c => new
                    {
                        TSICsubGroupID = c.Int(nullable: false, identity: true),
                        TSICgroupID = c.Int(nullable: false),
                        SubGroupCode = c.String(nullable: false, maxLength: 4),
                        SubGroupName = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.TSICsubGroupID)
                .ForeignKey("dbo.TSICgroup", t => t.TSICgroupID)
                .Index(t => t.TSICgroupID);
            
            CreateTable(
                "dbo.TSICgroup",
                c => new
                    {
                        TSICgroupID = c.Int(nullable: false, identity: true),
                        TSICsubCategoryID = c.Int(nullable: false),
                        GroupCode = c.String(nullable: false, maxLength: 3),
                        GroupName = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.TSICgroupID)
                .ForeignKey("dbo.TSICsubCategory", t => t.TSICsubCategoryID)
                .Index(t => t.TSICsubCategoryID);
            
            CreateTable(
                "dbo.TSICsubCategory",
                c => new
                    {
                        TSICsubCategoryID = c.Int(nullable: false, identity: true),
                        TSICcategoryID = c.Int(nullable: false),
                        SubCategoryCode = c.String(nullable: false, maxLength: 2),
                        SubCategoryName = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.TSICsubCategoryID)
                .ForeignKey("dbo.TSICcategory", t => t.TSICcategoryID)
                .Index(t => t.TSICcategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TSICcode", "TSICsubGroupID", "dbo.TSICsubGroup");
            DropForeignKey("dbo.TSICsubGroup", "TSICgroupID", "dbo.TSICgroup");
            DropForeignKey("dbo.TSICgroup", "TSICsubCategoryID", "dbo.TSICsubCategory");
            DropForeignKey("dbo.TSICsubCategory", "TSICcategoryID", "dbo.TSICcategory");
            DropIndex("dbo.TSICsubCategory", new[] { "TSICcategoryID" });
            DropIndex("dbo.TSICgroup", new[] { "TSICsubCategoryID" });
            DropIndex("dbo.TSICsubGroup", new[] { "TSICgroupID" });
            DropIndex("dbo.TSICcode", new[] { "TSICsubGroupID" });
            DropTable("dbo.TSICsubCategory");
            DropTable("dbo.TSICgroup");
            DropTable("dbo.TSICsubGroup");
            DropTable("dbo.TSICcode");
            DropTable("dbo.TSICcategory");
        }
    }
}
