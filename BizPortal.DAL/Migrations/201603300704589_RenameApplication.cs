namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameApplication : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE dbo.Application SET ApplicationSysName = N'�����蹢�ͺѧ�Ѻ��÷ӧҹ' WHERE ApplicationSysName = N'ŧ����¹����ҹ�ѭ���͹�Ź�'");
        }
        
        public override void Down()
        {
        }
    }
}
