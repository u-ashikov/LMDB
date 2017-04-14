namespace LMDB.Data.Seeders
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using Models;
    internal class AwardsSeeder
    {
        public static void Seed(MoviesContext context)
        {
            string[] file = {};
            try
            {
                file = File.ReadAllLines("../../../../LMDB.Data/Datasets/awards.csv");
            }
            catch (DirectoryNotFoundException){ }

            for (int i = 1; i < file.Length; i++)
            {
                var awardName = file[i].Split(',')[0];
                DateTime dateIntroduced;
                var isDate = DateTime.TryParseExact(file[i].Split(',')[1],
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateIntroduced);

                if (!isDate) continue;

                var award = new Award
                {
                    Name = awardName,
                    DateIntroduced = dateIntroduced
                };

                context.Awards.AddOrUpdate(a => a.Name, award);
            }

            context.SaveChanges();
        }
    }
}
