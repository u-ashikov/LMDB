﻿namespace LMDB.Data.Seeders
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
            var awards = context.AwardCategories.Local;

            var rand = new Random();
            for (int i = 1; i < actors.Length; i++)
            {
                var line = actors[i].Split(',');
                var firstName = line[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                var lastName = line[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                var country = context.Countries.Find(i * 2 % countriesLen + 1);
    
                var actor = new Actor
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Birthdate = new DateTime(rand.Next(1940, 2000), i % 12 + 1, rand.Next(1, 28)),
                    CountryId = country?.Id
                };

                if (i % 2 == 0)
                {
                    var awardIndex = rand.Next(0, awards.Count - 1);
                    actor.Awards.Add(awards[awardIndex]);
                }

                if (i % 3 == 0)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        var awardIndex = rand.Next(0, awards.Count - 1);
                        actor.Awards.Add(awards[awardIndex]);
                    }
                }

                if (i % 5 == 0)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        var awardIndex = rand.Next(0, awards.Count - 1);
                        actor.Awards.Add(awards[awardIndex]);
                    }
                }

                if (i % 10 == 0)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        var awardIndex = rand.Next(0, awards.Count - 1);
                        actor.Awards.Add(awards[awardIndex]);
                    }
                }

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
