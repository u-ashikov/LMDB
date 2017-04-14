namespace LMDB.Data.Seeders
{
    using System.Data.Entity.Migrations;
    using System.IO;
    using Models;
    using System.Web;
    using System.Linq;
    using System;

    internal class CountriesSeeder
    {
        public static void Seed(MoviesContext context)
        {
            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/countries.csv"));
            var countries = File.ReadAllText(docPath).Split(new[] {'\n','\r'},StringSplitOptions.RemoveEmptyEntries).ToArray();

            for (int i = 1; i < countries.Length; i++)
            {
                var country = new Country()
                {
                    Name = countries[i]
                };

                context.Countries.AddOrUpdate(c => c.Name, country);
            }

            context.SaveChanges();
        }
    }
}

