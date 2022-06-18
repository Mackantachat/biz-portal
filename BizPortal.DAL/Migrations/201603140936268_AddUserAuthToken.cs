namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAuthToken : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AuthToken", c => c.String(maxLength: 450));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "AuthToken");
        }
    }
}
