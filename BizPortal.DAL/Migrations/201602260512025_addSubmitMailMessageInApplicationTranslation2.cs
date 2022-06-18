namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubmitMailMessageInApplicationTranslation2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationTranslation", "SubmitMailMessage", c => c.String(storeType: "ntext"));
            DropColumn("dbo.ApplicationTranslation", "SubmitMaliMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationTranslation", "SubmitMaliMessage", c => c.String(storeType: "ntext"));
            DropColumn("dbo.ApplicationTranslation", "SubmitMailMessage");
        }
    }
}
