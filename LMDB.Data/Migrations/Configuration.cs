namespace LMDB.Data.Migrations
{
    using Seeders;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "LMDB.Data.MoviesContext";
        }

        protected override void Seed(MoviesContext context)
        {
            //CountriesSeeder.Seed(context);
            //AwardsSeeder.Seed(context);
            //CategoriesSeeder.Seed(context);
            //AwardCategoriesSeeder.Seed(context);
            //DirectorsSeeder.Seed(context);
            //ActorsSeeder.Seed(context);
            //MoviesSeeder.Seed(context);
        }
    }
}
