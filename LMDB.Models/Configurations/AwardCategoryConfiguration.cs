namespace LMDB.Models.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    public class AwardCategoryConfiguration : EntityTypeConfiguration<AwardCategory>
    {
        public AwardCategoryConfiguration()
        {
            HasMany(ac => ac.Actors)
                .WithMany(a => a.Awards)
                .Map(m =>
                {
                    m.ToTable("ActorAwards");
                    m.MapLeftKey("AwardCategoryId");
                    m.MapRightKey("ActorId");
                });

            HasMany(ac => ac.Directors)
                .WithMany(d => d.Awards)
                .Map(m =>
                {
                    m.ToTable("DirectorAwards");
                    m.MapLeftKey("AwardCategoryId");
                    m.MapRightKey("ADirectorId");
                });
        }
    }
}
