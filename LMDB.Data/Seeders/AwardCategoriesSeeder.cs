using LMDB.Models;
using System.Data.Entity.Migrations;

namespace LMDB.Data.Seeders
{
    internal class AwardCategoriesSeeder
    {
        public static void Seed(MoviesContext context)
        {
            var awards = context.Awards.Local;
            var categories = context.Categories.Local;

            foreach (var a in awards)
            {
                foreach (var c in categories)
                {
                    var awardCategory = new AwardCategory()
                    {
                        AwardId = a.Id,
                        CategoryId = c.Id
                    };

                    context.AwardCategories.AddOrUpdate(ac=>new { ac.AwardId,ac.CategoryId},awardCategory);
                }
            }

            context.SaveChanges();
        }
    }
}
