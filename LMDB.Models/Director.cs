namespace LMDB.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Directors")]
    public class Director : Contributor
    {
        public Director()
        {
            this.Awards = new HashSet<AwardCategory>();
            this.DirectedMovies = new HashSet<Movie>();
        }

        public virtual ICollection<AwardCategory> Awards { get; set; }

        public virtual ICollection<Movie> DirectedMovies { get; set; }
    }
}
