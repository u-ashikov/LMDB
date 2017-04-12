namespace LMDB.Models
{
    using System;
    using System.Collections.Generic;

    public class Award
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateIntroduced { get; set; }

        public virtual ICollection<AwardCategory> AwardCategories { get; set; } = new HashSet<AwardCategory>();
    }
}
