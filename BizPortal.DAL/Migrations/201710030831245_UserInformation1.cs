namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserInformation1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Prefix", c => c.String(maxLength: 450));
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String(maxLength: 450));
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String(maxLength: 450));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropColumn("dbo.AspNetUsers", "Prefix");
        }
    }
}
