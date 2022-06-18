namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberService2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberService", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.MemberService", "UserID");
            AddForeignKey("dbo.MemberService", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberService", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.MemberService", new[] { "UserID" });
            DropColumn("dbo.MemberService", "UserID");
        }
    }
}
