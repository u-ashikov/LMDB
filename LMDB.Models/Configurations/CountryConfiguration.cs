namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            this.HasMany(c => c.Users)
                .WithOptional(u => u.OriginCountry);

            this.HasMany(c => c.Directors)
                .WithOptional(d => d.Country);

            this.HasMany(c => c.Actors)
                .WithOptional(a => a.Country);
        }
    }
}
