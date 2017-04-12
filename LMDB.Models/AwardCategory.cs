namespace LMDB.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class AwardCategory
    {
        [Key, ForeignKey("Award"), Column(Order = 0)]
        public int AwardId { get; set; }
        public virtual Award Award { get; set; }

        [Key, ForeignKey("Category"), Column(Order = 1)]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();

        //public virtual ICollection<Actor> Actors { get; set; } = new HashSet<Actor>();

        // virtual ICollection<Director> Directors { get; set; } = new HashSet<Director>();
    }
}
