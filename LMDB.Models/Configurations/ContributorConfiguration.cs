namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    public class ContributorConfiguration : EntityTypeConfiguration<Contributor>
    {
        public ContributorConfiguration()
        {
            this.HasRequired(c => c.Country)
                .WithMany(country => country.Contributors)
                .WillCascadeOnDelete(false);
        }
    }
}
