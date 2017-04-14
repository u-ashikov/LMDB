namespace LMDB.Data
{
    using Migrations;
    using System.Data.Entity;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Configurations;

    public class MoviesContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesContext()
            : base("MoviesContext", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<MoviesContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MoviesContext, Configuration>());
        }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Award> Awards { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<AwardCategory> AwardCategories { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Director> Directors { get; set; }

        public virtual DbSet<Actor> Actors { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Configurations.Add(new CountryConfiguration());

            modelBuilder.Configurations.Add(new ReviewConfiguration());

            modelBuilder.Configurations.Add(new MovieConfiguration());

            modelBuilder.Configurations.Add(new AwardCategoryConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static MoviesContext Create()
        {
            return new MoviesContext();
        }
    }
}
