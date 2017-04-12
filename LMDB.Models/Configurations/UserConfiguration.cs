namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    public class UserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public UserConfiguration()
        {
            //this.HasMany(u => u.LikedMovies)
            //    .WithMany(m => m.Likes)
            //    .Map(m =>
            //    {
            //        m.ToTable("MovieLikes");
            //        m.MapLeftKey("UserId");
            //        m.MapRightKey("MovieId");
            //    });

            //this.HasMany(u => u.DislikedMovies)
            //    .WithMany(m => m.Dislikes)
            //    .Map(m =>
            //    {
            //        m.ToTable("MovieDislikes");
            //        m.MapLeftKey("UserId");
            //        m.MapRightKey("MovieId");
            //    });

            //this.HasMany(u => u.FavouriteMovies)
            //    .WithMany(m => m.MovieFans)
            //    .Map(m =>
            //    {
            //        m.ToTable("UserFavouriteMovies");
            //        m.MapLeftKey("UserId");
            //        m.MapRightKey("FavouriteMovieId");
            //    });

            this.HasMany(u => u.Comments)
                .WithRequired(c => c.Author)
                .WillCascadeOnDelete(false);
        }
    }
}
