namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSigningPerson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SigningPerson", "SignatureFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SigningPerson", "SignatureFileName");
        }
    }
}
