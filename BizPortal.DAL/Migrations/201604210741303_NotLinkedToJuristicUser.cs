namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotLinkedToJuristicUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JuristicApplicationStatusRequest", "NotLinkedToJuristicUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.JuristicApplicationStatusRequest", "NotLinkedToJuristicUser");
        }
    }
}
