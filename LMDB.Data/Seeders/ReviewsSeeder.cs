using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using LMDB.Models;

namespace LMDB.Data.Seeders
{
    internal class ReviewsSeeder
    {
        public static void Seed(MoviesContext context)
        {
            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (webRootPath == null) return;
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/users.csv"));
            var reviews = File.ReadAllText(docPath).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var authors = context.Users;
            var authorsLen = authors.Count();
            for (int i = 1; i < reviews.Length; i++)
            {
                var movieId = reviews[i].Split(',')[0];
                var content = reviews[i].Substring(reviews[i].IndexOf(',') + 1);

                var review = new Review
                {
                    Content = content,
                    ReviewedMovieId = int.Parse(movieId),
                    AuthorId = authors.Find(i % authorsLen + 1).Id,
                    DatePublished = DateTime.Now
                };
            

                context.Reviews.AddOrUpdate(r => r.Content, review);
            }

            context.SaveChanges();
        }
    }
}
