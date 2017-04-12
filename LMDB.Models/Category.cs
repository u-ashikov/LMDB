namespace LMDB.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<AwardCategory> AwardCategories { get; set; } = new HashSet<AwardCategory>();
    }
}
