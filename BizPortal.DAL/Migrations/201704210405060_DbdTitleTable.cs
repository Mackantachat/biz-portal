namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbdTitleTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbdTitle",
                c => new
                    {
                        DbdTitleID = c.Int(nullable: false, identity: true),
                        TitleName = c.String(nullable: false, maxLength: 450),
                        TitleAbbreviation = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.DbdTitleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DbdTitle");
        }
    }
}
