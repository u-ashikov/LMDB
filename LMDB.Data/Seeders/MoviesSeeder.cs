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
                File.ReadAllText(docPath).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var directors = context.Directors;
            var directorsCount = directors.Count();

            var awards = context.AwardCategories.Local;

            var actors = context.Actors;
            var actorsCount = actors.Count();

            var rand = new Random();

            for (int i = 1; i < movies.Length; i++)
            {
                var line = movies[i].Split(',');
                var indexOfYear = line[0].IndexOf('(');
                var title = line[0].Substring(0, indexOfYear - 1);
                var year = int.Parse(line[0].Substring(indexOfYear + 1, 4));
                var director = directors.Find(i % directorsCount + 1);

                var movie = new Movie
                {
                    Title = title,
                    DateReleased = new DateTime(year, rand.Next(1, 12), rand.Next(1, 28)),
                    DirectorId = director.Id
                };
               

                if (director == null) continue;

                var genres = line[1].Split('|');

               foreach (var genreName in genres)
               {
                   var genre = context.Genres.FirstOrDefault(g => g.Name == genreName);


                   movie.Genres.Add(genre == null ? new Genre { Name = genreName }:genre);
               }

                for (int j = 0; j < 6; j++)
                {
                    var actor = actors.Find(j * i % actorsCount + 1);
                    movie.Actors.Add(actor);
                }

                if (i % 2 == 0)
                {
                    var awardIndex = rand.Next(0, awards.Count - 1);
                    movie.Awards.Add(awards[awardIndex]);
                }

                context.Movies.AddOrUpdate(m => m.Title, movie);
                context.SaveChanges();
            }        
        }
    }
}
