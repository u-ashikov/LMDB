namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDirectorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Biography = c.String(),
                        Birthdate = c.DateTime(),
                        Picture = c.Binary(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.DirectorAwards",
                c => new
                    {
                        AwardCategoryId = c.Int(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AwardCategoryId, t.DirectorId })
                .ForeignKey("dbo.AwardCategories", t => t.AwardCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.AwardCategoryId)
                .Index(t => t.DirectorId);
            
            CreateIndex("dbo.Movies", "DirectorId");
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DirectorAwards", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.DirectorAwards", "AwardCategoryId", "dbo.AwardCategories");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.Directors", "CountryId", "dbo.Countries");
            DropIndex("dbo.DirectorAwards", new[] { "DirectorId" });
            DropIndex("dbo.DirectorAwards", new[] { "AwardCategoryId" });
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropIndex("dbo.Directors", new[] { "CountryId" });
            DropTable("dbo.DirectorAwards");
            DropTable("dbo.Directors");
        }
    }
}
