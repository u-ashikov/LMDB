namespace LMDB.Models
{
    using System.Collections.Generic;

    public class AwardCategory
    {
        public int Id { get; set; }

        public int AwardId { get; set; }
        public virtual Award Award { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();

        public virtual ICollection<Contributor> Actors { get; set; } = new HashSet<Contributor>();

        public virtual ICollection<Contributor> Directors { get; set; } = new HashSet<Contributor>();
    }
}
