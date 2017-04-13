namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            this.HasRequired(r => r.Author)
                .WithMany(a => a.ReviewsWrited)
                .HasForeignKey(r=>r.AuthorId)
                .WillCascadeOnDelete(false);

            this.HasRequired(r => r.ReviewedMovie)
                .WithOptional(m => m.Review);

            this.HasKey(r => r.ReviewedMovieId);
        }
    }
}
