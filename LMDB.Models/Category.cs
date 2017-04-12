namespace LMDB.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AwardCategory> AwardCategories { get; set; } = new HashSet<AwardCategory>();
    }
}
