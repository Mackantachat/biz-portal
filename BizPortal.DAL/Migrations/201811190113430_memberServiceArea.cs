namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberServiceArea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MemberServiceArea", "Province", c => c.String());
            AddColumn("dbo.MemberServiceArea", "District", c => c.String());
            AddColumn("dbo.MemberServiceArea", "Section", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MemberServiceArea", "Section");
            DropColumn("dbo.MemberServiceArea", "District");
            DropColumn("dbo.MemberServiceArea", "Province");
        }
    }
}
