namespace LMDB.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AwardCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AwardId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Awards", t => t.AwardId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.AwardId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Contributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Biography = c.String(nullable: false),
                        Gender = c.Int(nullable: false),
                        Birthdate = c.DateTime(),
                        Picture = c.Binary(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 255),
                        DateReleased = c.DateTime(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Directors", t => t.DirectorId)
                .Index(t => t.DirectorId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 1000),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                        CommentedMovieId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Movies", t => t.CommentedMovieId)
                .Index(t => t.AuthorId)
                .Index(t => t.CommentedMovieId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Gender = c.Int(nullable: false),
                        OriginCountryId = c.Int(),
                        ProfilePicture = c.Binary(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.OriginCountryId)
                .Index(t => t.OriginCountryId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewedMovieId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        DatePublished = c.DateTime(nullable: false),
                        AuthorId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ReviewedMovieId)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Movies", t => t.ReviewedMovieId)
                .Index(t => t.ReviewedMovieId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Awards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        DateIntroduced = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
            CreateTable(
                "dbo.MovieAwards",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        AwardCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.AwardCategoryId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.AwardCategories", t => t.AwardCategoryId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.AwardCategoryId);
            
            CreateTable(
                "dbo.MovieDislikes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.UserFavouriteMovies",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FavouriteMovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FavouriteMovieId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.FavouriteMovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.FavouriteMovieId);
            
            CreateTable(
                "dbo.MovieLikes",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.MovieId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.MovieGenres",
                c => new
                    {
                        MovieId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieId, t.GenreId })
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.GenreId);
            
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
                "dbo.DirectorAwards",
                c => new
                    {
                        AwardCategoryId = c.Int(nullable: false),
                        ADirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AwardCategoryId, t.ADirectorId })
                .ForeignKey("dbo.AwardCategories", t => t.AwardCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Directors", t => t.ADirectorId, cascadeDelete: true)
                .Index(t => t.AwardCategoryId)
                .Index(t => t.ADirectorId);
            
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contributors", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Directors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contributors", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Directors", "Id", "dbo.Contributors");
            DropForeignKey("dbo.Actors", "Id", "dbo.Contributors");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DirectorAwards", "ADirectorId", "dbo.Directors");
            DropForeignKey("dbo.DirectorAwards", "AwardCategoryId", "dbo.AwardCategories");
            DropForeignKey("dbo.AwardCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AwardCategories", "AwardId", "dbo.Awards");
            DropForeignKey("dbo.ActorAwards", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.ActorAwards", "AwardCategoryId", "dbo.AwardCategories");
            DropForeignKey("dbo.AspNetUsers", "OriginCountryId", "dbo.Countries");
            DropForeignKey("dbo.Contributors", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.MovieGenres", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.MovieGenres", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropForeignKey("dbo.Comments", "CommentedMovieId", "dbo.Movies");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "ReviewedMovieId", "dbo.Movies");
            DropForeignKey("dbo.Reviews", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MovieLikes", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieLikes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserFavouriteMovies", "FavouriteMovieId", "dbo.Movies");
            DropForeignKey("dbo.UserFavouriteMovies", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MovieDislikes", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieDislikes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.MovieAwards", "AwardCategoryId", "dbo.AwardCategories");
            DropForeignKey("dbo.MovieAwards", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieActors", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "MovieId", "dbo.Movies");
            DropIndex("dbo.Directors", new[] { "Id" });
            DropIndex("dbo.Actors", new[] { "Id" });
            DropIndex("dbo.DirectorAwards", new[] { "ADirectorId" });
            DropIndex("dbo.DirectorAwards", new[] { "AwardCategoryId" });
            DropIndex("dbo.ActorAwards", new[] { "ActorId" });
            DropIndex("dbo.ActorAwards", new[] { "AwardCategoryId" });
            DropIndex("dbo.MovieGenres", new[] { "GenreId" });
            DropIndex("dbo.MovieGenres", new[] { "MovieId" });
            DropIndex("dbo.MovieLikes", new[] { "MovieId" });
            DropIndex("dbo.MovieLikes", new[] { "UserId" });
            DropIndex("dbo.UserFavouriteMovies", new[] { "FavouriteMovieId" });
            DropIndex("dbo.UserFavouriteMovies", new[] { "UserId" });
            DropIndex("dbo.MovieDislikes", new[] { "MovieId" });
            DropIndex("dbo.MovieDislikes", new[] { "UserId" });
            DropIndex("dbo.MovieAwards", new[] { "AwardCategoryId" });
            DropIndex("dbo.MovieAwards", new[] { "MovieId" });
            DropIndex("dbo.MovieActors", new[] { "ActorId" });
            DropIndex("dbo.MovieActors", new[] { "MovieId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "AuthorId" });
            DropIndex("dbo.Reviews", new[] { "ReviewedMovieId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "OriginCountryId" });
            DropIndex("dbo.Comments", new[] { "CommentedMovieId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropIndex("dbo.Contributors", new[] { "CountryId" });
            DropIndex("dbo.AwardCategories", new[] { "CategoryId" });
            DropIndex("dbo.AwardCategories", new[] { "AwardId" });
            DropTable("dbo.Directors");
            DropTable("dbo.Actors");
            DropTable("dbo.DirectorAwards");
            DropTable("dbo.ActorAwards");
            DropTable("dbo.MovieGenres");
            DropTable("dbo.MovieLikes");
            DropTable("dbo.UserFavouriteMovies");
            DropTable("dbo.MovieDislikes");
            DropTable("dbo.MovieAwards");
            DropTable("dbo.MovieActors");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.Awards");
            DropTable("dbo.Genres");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Movies");
            DropTable("dbo.Countries");
            DropTable("dbo.Contributors");
            DropTable("dbo.AwardCategories");
        }
    }
}
