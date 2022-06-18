namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReceiptRunning : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReceiptRunningTransaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrgCode = c.String(nullable: false, maxLength: 20),
                        RunningNumber = c.Int(nullable: false),
                        ApplicationRequestNumber = c.String(nullable: false, maxLength: 20),
                        Filename = c.String(nullable: false, maxLength: 100),
                        FileId = c.String(nullable: false, maxLength: 50),
                        Memo = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReceiptRunningTransaction");
        }
    }
}
