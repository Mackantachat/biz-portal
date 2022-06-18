namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastestLoginProvider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastestLoginProvider", c => c.String(maxLength: 450));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastestLoginProvider");
        }
    }
}
