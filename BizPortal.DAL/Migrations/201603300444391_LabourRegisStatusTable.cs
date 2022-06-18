namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LabourRegisStatusTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LabourRegisStatus",
                c => new
                    {
                        LabourRegisStatusID = c.Int(nullable: false, identity: true),
                        JuristicID = c.String(nullable: false, maxLength: 13),
                        RefCode = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        TransactionTimestamp = c.Long(nullable: false),
                        Status = c.Boolean(nullable: false),
                        ApplicationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LabourRegisStatusID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .Index(t => t.ApplicationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LabourRegisStatus", "ApplicationID", "dbo.Application");
            DropIndex("dbo.LabourRegisStatus", new[] { "ApplicationID" });
            DropTable("dbo.LabourRegisStatus");
        }
    }
}
