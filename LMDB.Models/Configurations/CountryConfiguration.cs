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

            this.HasMany(c => c.Actors)
                .WithOptional(mc => mc.Country)
                .HasForeignKey(a=>a.CountryId)
                .WillCascadeOnDelete(false);

            //this.HasMany(c => c.Directors)
            //    .WithOptional(mc => mc.Country)
            //    .HasForeignKey(d=>d.CountryId)
            //    .WillCascadeOnDelete(false);
        }
    }
}
