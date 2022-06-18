namespace BizPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoiPostalCodeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MoiPostalCode",
                c => new
                    {
                        MoiPostalCodeID = c.Int(nullable: false, identity: true),
                        GeoCode = c.String(nullable: false, maxLength: 6),
                        PostalCode = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.MoiPostalCodeID)
                .Index(t => t.GeoCode, name: "IX_GEOCODE")
                .Index(t => t.PostalCode, name: "IX_POSTALCODE");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MoiPostalCode", "IX_POSTALCODE");
            DropIndex("dbo.MoiPostalCode", "IX_GEOCODE");
            DropTable("dbo.MoiPostalCode");
        }
    }
}
