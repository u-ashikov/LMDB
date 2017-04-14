using System.Data.Entity.Validation;

namespace LMDB.Data.Seeders
{
    using Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    internal class DirectorsSeeder
    {
        public static void Seed(MoviesContext context)
        {
            var random = new Random();

            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (webRootPath == null) return;
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/directors.csv"));
            var directors = File.ReadAllText(docPath).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            var awards = context.AwardCategories.Local;
            var countries = context.Countries.Local;

            for (int i = 1; i < directors.Length; i++)
            {
                var directorInfo = directors[i].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var firstName = directorInfo[0];
                var lastName = directorInfo[1];

                var countryIndex = random.Next(0, countries.Count - 1);

                var director = new Director
                {
                    FirstName = firstName,
                    LastName = lastName,
                    CountryId = countries[countryIndex].Id
                };

                //if (i % 2 == 0)
                //{
                //    var awardIndex = random.Next(0, awards.Count - 1);
                //    director.Awards.Add(awards[awardIndex]);
                //}

                context.Directors.AddOrUpdate(d=> new { d.FirstName,d.LastName},director);
            }

            context.SaveChanges();

        }
    }
}
