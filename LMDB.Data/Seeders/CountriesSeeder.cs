namespace LMDB.Data.Seeders
{
    using System.Data.Entity.Migrations;
    using System.IO;
    using Models;
    internal class CountriesSeeder
    {
        public static void Seed(MoviesContext context)
        {
            string[] file;
            try
            {
                file = File.ReadAllLines("../../../../LMDB.Data/Datasets/countries.csv");
            }
            catch (DirectoryNotFoundException)
            {
                file = new string[] { };
            }

            for (int i = 1; i < file.Length; i++)
            {
                var line = file[i].Split(',');
                var countryName = line[0];

                var country = new Country
                {
                    Name = countryName
                };

                context.Countries.AddOrUpdate(c => c.Name, country);
            }

            context.SaveChanges();
        }
    }
}

