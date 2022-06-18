namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDBDCommerceTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DBDCommerceObjective", "DisplayObjectiveTH", c => c.String());
            AddColumn("dbo.DBDCommerceObjective", "CommerceFlag", c => c.String(maxLength: 1));
            AddColumn("dbo.DBDCommerceOffice", "DisplayOfficeTH", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DBDCommerceOffice", "DisplayOfficeTH");
            DropColumn("dbo.DBDCommerceObjective", "CommerceFlag");
            DropColumn("dbo.DBDCommerceObjective", "DisplayObjectiveTH");
        }
    }
}
