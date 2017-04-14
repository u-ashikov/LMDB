namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActorModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
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
                "dbo.ActorAwards",
                c => new
                    {
                        AwardCategoryId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AwardCategoryId, t.ActorId })
                .ForeignKey("dbo.AwardCategories", t => t.AwardCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.AwardCategoryId)
                .Index(t => t.ActorId);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.ActorId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Actors", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.ActorAwards", "AwardCategoryId", "dbo.AwardCategories");
            DropIndex("dbo.MovieActors", new[] { "ActorId" });
            DropIndex("dbo.MovieActors", new[] { "MovieId" });
            DropIndex("dbo.ActorAwards", new[] { "ActorId" });
            DropIndex("dbo.ActorAwards", new[] { "AwardCategoryId" });
            DropIndex("dbo.Actors", new[] { "CountryId" });
            DropTable("dbo.MovieActors");
            DropTable("dbo.ActorAwards");
            DropTable("dbo.Actors");
        }
    }
}
