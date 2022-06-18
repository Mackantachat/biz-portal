namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterMailMessage2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationTranslation", "ApprovedMailMessage", c => c.String(storeType: "ntext"));
            AddColumn("dbo.ApplicationTranslation", "RejectedMaliMessage", c => c.String(storeType: "ntext"));
            DropColumn("dbo.ApplicationTranslation", "MailMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationTranslation", "MailMessage", c => c.String(storeType: "ntext"));
            DropColumn("dbo.ApplicationTranslation", "RejectedMaliMessage");
            DropColumn("dbo.ApplicationTranslation", "ApprovedMailMessage");
        }
    }
}
