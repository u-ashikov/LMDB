namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    public class MovieConfiguration : EntityTypeConfiguration<Movie>
    {
        public MovieConfiguration()
        {
            Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(255);

            //HasRequired(m => m.Director)
            //    .WithMany(d => d.DirectedMovies)
            //    .HasForeignKey(m=>m.DirectorId)
            //    .WillCascadeOnDelete(false);

            HasMany(m => m.Awards)
                .WithMany(a => a.Movies)
                .Map(m =>
                {
                    m.ToTable("MovieAwards");
                    m.MapLeftKey("MovieId");
                    m.MapRightKey("AwardCategoryId");
                });

            HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .Map(m =>
                {
                    m.ToTable("MovieGenres");
                    m.MapLeftKey("MovieId");
                    m.MapRightKey("GenreId");
                });

            HasMany(m => m.Actors)
                .WithMany(g => g.ParticipatedMovies)
                .Map(m =>
                {
                    m.ToTable("MovieActors");
                    m.MapLeftKey("MovieId");
                    m.MapRightKey("ActorId");
                });

            HasMany(m => m.Comments)
                .WithRequired(g => g.CommentedMovie)
                .WillCascadeOnDelete(false);
        }
    }
}
