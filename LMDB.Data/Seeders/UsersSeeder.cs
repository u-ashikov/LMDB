using System;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using LMDB.Models;
using LMDB.Models.Enums;

namespace LMDB.Data.Seeders
{
    internal class UsersSeeder
    {
        public static void Seed(MoviesContext context)
        {
            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (webRootPath == null) return;
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/users.csv"));
            var users = File.ReadAllText(docPath).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            var countriesLen = context.Countries.Count();
            var moviesCount = context.Movies.Count();

            for (int i = 1; i < users.Length; i++)
            {
                var line = users[i].Split(',');
                var username = line[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                var firstName = line[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                var lastName = line[2].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                var genderCode = line[3];
                var email = line[4];
                var country = context.Countries.Find(i * 2 % countriesLen + 1);
              
                var user = new ApplicationUser
                {
                    UserName = username,
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = (Gender) int.Parse(genderCode),
                    OriginCountryId = country.Id,
                    Email = email,
                    PasswordHash = "PASSW0RDH1SH"
                };
                if (i%3 == 0)
                {
                    for (int j = 1; j < 25; j++)
                    {
                        user.DislikedMovies.Add(context.Movies.Find(i * j * 3 %moviesCount + 1));
                    }
                }
                else
                {
                    for (int j = 0; j < i * 5 % 50; j++)
                    {
                        user.LikedMovies.Add(context.Movies.Find(i * j * 2 % moviesCount + 1));
                    }
                }

                context.Users.AddOrUpdate(u => u.UserName, user);
            }

            context.SaveChanges();
        }
    }
}
