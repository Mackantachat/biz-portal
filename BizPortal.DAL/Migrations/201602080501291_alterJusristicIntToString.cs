namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterJusristicIntToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JuristicApplicationStatusRequest", "JuristicID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JuristicApplicationStatusRequest", "JuristicID", c => c.Int(nullable: false));
        }
    }
}
