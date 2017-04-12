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

        public virtual ICollection<Actor> Actors { get; set; } = new HashSet<Actor>();

        public virtual ICollection<Director> Directors { get; set; } = new HashSet<Director>();
    }
}
