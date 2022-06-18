namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAlias : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Article", new[] { "Alias" });
            DropIndex("dbo.Category", new[] { "Alias" });
            DropIndex("dbo.Section", new[] { "Alias" });
            DropColumn("dbo.Article", "Alias");
            DropColumn("dbo.Category", "Alias");
            DropColumn("dbo.Section", "Alias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Section", "Alias", c => c.String(nullable: false, maxLength: 450));
            AddColumn("dbo.Category", "Alias", c => c.String(nullable: false, maxLength: 450));
            AddColumn("dbo.Article", "Alias", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.Section", "Alias", unique: true);
            CreateIndex("dbo.Category", "Alias", unique: true);
            CreateIndex("dbo.Article", "Alias", unique: true);
        }
    }
}
