namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    public class ReviewConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewConfiguration()
        {
            this.HasRequired(r => r.Author)
                .WithMany(a => a.ReviewsWrited)
                .WillCascadeOnDelete(false);

            this.HasRequired(r => r.ReviewedMovie)
                .WithMany(m => m.Reviews)
                .WillCascadeOnDelete(false);
        }
    }
}
