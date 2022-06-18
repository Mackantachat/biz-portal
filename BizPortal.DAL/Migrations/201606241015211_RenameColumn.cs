namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumn : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ApplicationTranslation", "RejectedMaliMessage", "RejectedMailMessage");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.ApplicationTranslation", "RejectedMailMessage", "RejectedMaliMessage");
        }
    }
}
