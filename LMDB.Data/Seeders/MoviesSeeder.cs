namespace LMDB.Data.Seeders
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Models;
    internal class MoviesSeeder
    {
        public static void Seed(MoviesContext context)
        {
            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (webRootPath == null) return;
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/movies.csv"));
            var movies =
                File.ReadAllText(docPath).Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var rand = new Random();
            for (int i = 1; i < movies.Length; i++)
            {
                var line = movies[i].Split(',');
                var indexOfYear = line[0].IndexOf('(');
                var title = line[0].Substring(0, indexOfYear);
                var year = int.Parse(line[0].Substring(indexOfYear + 1, line[0].Length - indexOfYear));
                var movie = new Movie
                {
                    Title = title,
                    DateReleased = new DateTime(year, rand.Next(1, 12), rand.Next(1, 28))
                };

                context.Movies.AddOrUpdate(m => m.Title, movie);
            }

            context.SaveChanges();
        }
    }
}
