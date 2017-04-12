namespace LMDB.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;

    public class Award
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        public DateTime DateIntroduced { get; set; }

        public virtual ICollection<AwardCategory> AwardCategories { get; set; } = new HashSet<AwardCategory>();
    }
}
