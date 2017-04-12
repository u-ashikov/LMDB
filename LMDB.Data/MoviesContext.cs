namespace LMDB.Data
{
    using System.Data.Entity;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;

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

        public static MoviesContext Create()
        {
            return new MoviesContext();
        }
    }
}
