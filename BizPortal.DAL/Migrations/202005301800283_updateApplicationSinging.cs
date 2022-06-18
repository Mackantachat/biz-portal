namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateApplicationSinging : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SigningPerson", new[] { "Application_ApplicationID" });
            RenameColumn(table: "dbo.SigningPerson", name: "Application_ApplicationID", newName: "ApplicationID");
            AlterColumn("dbo.SigningPerson", "ApplicationID", c => c.Int(nullable: false));
            CreateIndex("dbo.SigningPerson", "ApplicationID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SigningPerson", new[] { "ApplicationID" });
            AlterColumn("dbo.SigningPerson", "ApplicationID", c => c.Int());
            RenameColumn(table: "dbo.SigningPerson", name: "ApplicationID", newName: "Application_ApplicationID");
            CreateIndex("dbo.SigningPerson", "Application_ApplicationID");
        }
    }
}
