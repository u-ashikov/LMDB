namespace LMDB.Data
{
    using System.Data.Entity;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Configurations;

    public class MoviesContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesContext()
            : base("MoviesContext", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Movie> Movies { get; set; }

        public virtual DbSet<Award> Awards { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<AwardCategory> AwardCategories { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Contributor> Contributors { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());

            modelBuilder.Configurations.Add(new ContributorConfiguration());

            modelBuilder.Configurations.Add(new CountryConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public static MoviesContext Create()
        {
            return new MoviesContext();
        }
    }
}
