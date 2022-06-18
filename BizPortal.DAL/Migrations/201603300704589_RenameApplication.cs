namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameApplication : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Application SET ApplicationSysName = N'การยื่นข้อบังคับการทำงาน' WHERE ApplicationSysName = N'ลงทะเบียนขอใช้งานบัญชีออนไลน์'");
        }
        
        public override void Down()
        {
        }
    }
}
