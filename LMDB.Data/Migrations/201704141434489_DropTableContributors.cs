namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTableContributors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ActorAwards", "AwardCategoryId", "dbo.AwardCategories");
            DropForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.Contributors", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MovieActors", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.DirectorAwards", "AwardCategoryId", "dbo.AwardCategories");
            DropForeignKey("dbo.DirectorAwards", "ADirectorId", "dbo.Directors");
            DropForeignKey("dbo.Actors", "Id", "dbo.Contributors");
            DropForeignKey("dbo.Directors", "Id", "dbo.Contributors");
            DropIndex("dbo.Contributors", new[] { "CountryId" });
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropIndex("dbo.ActorAwards", new[] { "AwardCategoryId" });
            DropIndex("dbo.ActorAwards", new[] { "ActorId" });
            DropIndex("dbo.MovieActors", new[] { "MovieId" });
            DropIndex("dbo.MovieActors", new[] { "ActorId" });
            DropIndex("dbo.DirectorAwards", new[] { "AwardCategoryId" });
            DropIndex("dbo.DirectorAwards", new[] { "ADirectorId" });
            DropIndex("dbo.Actors", new[] { "Id" });
            DropIndex("dbo.Directors", new[] { "Id" });
            DropTable("dbo.Contributors");
            DropTable("dbo.ActorAwards");
            DropTable("dbo.MovieActors");
            DropTable("dbo.DirectorAwards");
            DropTable("dbo.Actors");
            DropTable("dbo.Directors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DirectorAwards",
                c => new
                    {
                        AwardCategoryId = c.Int(nullable: false),
                        ADirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AwardCategoryId, t.ADirectorId });
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.ActorId });
            
            CreateTable(
                "dbo.ActorAwards",
                c => new
                    {
                        AwardCategoryId = c.Int(nullable: false),
                        ActorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AwardCategoryId, t.ActorId });
            
            CreateTable(
                "dbo.Contributors",
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Directors", "Id");
            CreateIndex("dbo.Actors", "Id");
            CreateIndex("dbo.DirectorAwards", "ADirectorId");
            CreateIndex("dbo.DirectorAwards", "AwardCategoryId");
            CreateIndex("dbo.MovieActors", "ActorId");
            CreateIndex("dbo.MovieActors", "MovieId");
            CreateIndex("dbo.ActorAwards", "ActorId");
            CreateIndex("dbo.ActorAwards", "AwardCategoryId");
            CreateIndex("dbo.Movies", "DirectorId");
            CreateIndex("dbo.Contributors", "CountryId");
            AddForeignKey("dbo.Directors", "Id", "dbo.Contributors", "Id");
            AddForeignKey("dbo.Actors", "Id", "dbo.Contributors", "Id");
            AddForeignKey("dbo.DirectorAwards", "ADirectorId", "dbo.Directors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DirectorAwards", "AwardCategoryId", "dbo.AwardCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "DirectorId", "dbo.Directors", "Id");
            AddForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MovieActors", "MovieId", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Contributors", "CountryId", "dbo.Countries", "Id");
            AddForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ActorAwards", "AwardCategoryId", "dbo.AwardCategories", "Id", cascadeDelete: true);
        }
    }
}
