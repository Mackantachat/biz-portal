namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addServiceCategoryList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SerivceCategoryList",
                c => new
                    {
                        SerivceCategoryCODE = c.String(nullable: false, maxLength: 10),
                        SerivceCategoryDescription = c.String(nullable: false),
                        SerivceCategoryUrl = c.String(nullable: false),
                        SerivceCategoryGroup = c.String(),
                        ConsumerKey = c.String(nullable: false),
                        ConsumerSecret = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SerivceCategoryCODE);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SerivceCategoryList");
        }
    }
}
