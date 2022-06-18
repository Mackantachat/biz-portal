namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSubmitMailMessageInApplicationTranslation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationTranslation", "SubmitMaliMessage", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationTranslation", "SubmitMaliMessage");
        }
    }
}
