namespace LMDB.Data.Seeders
{
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using Models;

    internal class CategoriesSeeder
    {
        public static void Seed(MoviesContext context)
        {
            string[] file = {};
            try
            {
                file = File.ReadAllLines("../../../../LMDB.Data/Datasets/categories.csv");
            }
            catch (DirectoryNotFoundException) { }

            for (int i = 1; i < file.Length; i++)
            {
                var line = file[i].Split(',');
                var name = line[0];
                var description = string.Join(", ", file[i].Split(',').Skip(1));

                var category = new Category
                {
                    Name = name,
                    Description = description
                };

                context.Categories.AddOrUpdate(c => c.Name, category);
            }

            context.SaveChanges();
        }
    }
}
