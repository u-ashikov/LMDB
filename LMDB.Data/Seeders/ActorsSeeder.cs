namespace LMDB.Data.Seeders
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Models;
    internal class ActorsSeeder
    {
        public static void Seed(MoviesContext context)
        {
            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (webRootPath == null) return;
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/actors.csv"));
            var actors = File.ReadAllText(docPath).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var countriesLen = context.Countries.Count();
            var rand = new Random();
            for (int i = 1; i < actors.Length; i++)
            {
                var line = actors[i].Split(',');
                var firstName = line[0].Split(new[] {' '},StringSplitOptions.RemoveEmptyEntries)[0];
                var lastName = line[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                var country = context.Countries.Find(i*2%countriesLen + 1);
                if (country == null) return;
                var actor = new Actor
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Birthdate = new DateTime(rand.Next(1940, 2000), i % 12 + 1, rand.Next(1, 28)),
                    CountryId = country.Id
                };

                context.Actors.AddOrUpdate(a => new
                {
                    a.FirstName,
                    a.LastName
                }, actor);
            }
            context.SaveChanges();
        }
    }
}
