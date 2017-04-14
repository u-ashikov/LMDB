namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            this.HasMany(c => c.Users)
                .WithOptional(u => u.OriginCountry)
                .WillCascadeOnDelete(false);

            this.HasMany(c => c.MovieContributors)
                .WithOptional(mc => mc.Country)
                .WillCascadeOnDelete(false);
        }
    }
}
