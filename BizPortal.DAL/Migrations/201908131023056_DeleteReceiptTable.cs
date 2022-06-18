namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteReceiptTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ReceiptRunningTransaction");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReceiptRunningTransaction",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrgCode = c.String(),
                        RunningNumber = c.Int(nullable: false),
                        ApplicationRequestNumber = c.String(),
                        Filename = c.String(),
                        FileId = c.String(),
                        Memo = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        UpdateDate = c.DateTime(),
                        ActiveFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
