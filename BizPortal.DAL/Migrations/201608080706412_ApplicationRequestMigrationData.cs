namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationRequestMigrationData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JuristicApplicationStatusRequest", "MigratedToMongoDB", c => c.Boolean(nullable: false));
            AddColumn("dbo.JuristicApplicationStatusRequest", "MigratedDate", c => c.DateTime());
            AddColumn("dbo.JuristicApplicationStatusRequest", "MigratedRequestID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JuristicApplicationStatusRequest", "MigratedRequestID");
            DropColumn("dbo.JuristicApplicationStatusRequest", "MigratedDate");
            DropColumn("dbo.JuristicApplicationStatusRequest", "MigratedToMongoDB");
        }
    }
}
