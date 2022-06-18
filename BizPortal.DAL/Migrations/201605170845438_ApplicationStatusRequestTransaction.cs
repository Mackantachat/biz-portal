namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationStatusRequestTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JuristicApplicationStatusRequestLog", "TransactionCode", c => c.String(maxLength: 450));
            AddColumn("dbo.JuristicApplicationStatusRequest", "TransactionCode", c => c.String(maxLength: 450));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JuristicApplicationStatusRequest", "TransactionCode");
            DropColumn("dbo.JuristicApplicationStatusRequestLog", "TransactionCode");
        }
    }
}
