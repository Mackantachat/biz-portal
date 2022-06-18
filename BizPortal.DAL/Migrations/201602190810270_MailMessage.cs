namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MailMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationTranslation", "MailMessage", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationTranslation", "MailMessage");
        }
    }
}
