namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class role1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetRoles", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetRoles", "Order", c => c.Int(nullable: false));
        }
    }
}
