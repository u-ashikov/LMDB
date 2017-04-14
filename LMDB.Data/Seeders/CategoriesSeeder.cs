using System;

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
            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            if (webRootPath == null) return;
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/categories.csv"));
            var categories = File.ReadAllText(docPath).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

            for (int i = 1; i < categories.Length; i++)
            {
                var line = categories[i].Split(',');
                var name = line[0];
                var description = string.Join(", ", categories[i].Split(',').Skip(1));

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
