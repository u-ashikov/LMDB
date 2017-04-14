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
            var webRootPath = System.Web.Hosting.HostingEnvironment.MapPath("~");
            var docPath = Path.GetFullPath(Path.Combine(webRootPath, "../LMDB.Data/Datasets/awards.csv"));
            var awards = File.ReadAllText(docPath).Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < awards.Length; i++)
            {
                var awardInfo = awards[i].Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

                var awardName = awardInfo[0];
                var dateIntroduced = DateTime.ParseExact(awardInfo[1],"dd/MM/yyyy",CultureInfo.InvariantCulture);

                var award = new Award()
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
