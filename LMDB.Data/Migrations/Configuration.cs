namespace LMDB.Data.Migrations
{
    using Seeders;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LMDB.Data.MoviesContext";
        }

        protected override void Seed(MoviesContext context)
        {
            CountriesSeeder.Seed(context);
            AwardsSeeder.Seed(context);
            CategoriesSeeder.Seed(context);
            GenresSeeder.Seed(context);
            DirectorsSeeder.Seed(context);
            ActorsSeeder.Seed(context);
            MoviesSeeder.Seed(context);
        }
    }
}
