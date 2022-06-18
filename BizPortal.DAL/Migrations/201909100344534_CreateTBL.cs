namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTBL : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Business",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AppSysName = c.String(nullable: false),
                        BusinessSysName = c.String(nullable: false),
                        BusinessNameTH = c.String(),
                        BusinessNameEN = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Business");
        }
    }
}
