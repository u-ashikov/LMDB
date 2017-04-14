﻿namespace LMDB.Data.Seeders
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Models;
    internal class DirectorsSeeder
    {
        public static void Seed(MoviesContext context)
        {
            string[] file;
            try
            {
                file = File.ReadAllLines("../../../../LMDB.Data/Datasets/directors.csv");
            }
            catch (DirectoryNotFoundException)
            {
                file = new string[] { };
            }

            var countriesLen = context.Countries.Count();
            var rand = new Random();
            for (int i = 1; i < file.Length; i++)
            {
                var line = file[i].Split(',');
                var firstName = line[0].Split(' ')[0];
                var lastName = line[0].Split(' ')[1];

                var country = context.Countries.Find(i * 2 % countriesLen + 1);

                var director = new Director
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Birthdate = new DateTime(rand.Next(1940, 2000), i % 12 + 1, rand.Next(1, 28)),
                    Country = country
                };

                context.Directors.AddOrUpdate(d => new
                {
                    d.FirstName,
                    d.LastName
                }, director);
            }

            context.SaveChanges();
        }
    }
}
