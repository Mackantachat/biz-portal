namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntroText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleTranslation", "ArticleIntroText", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleTranslation", "ArticleIntroText");
        }
    }
}
