namespace LMDB.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Actors")]
    public class Actor : Contributor
    {
        public Actor()
        {
            this.Awards = new HashSet<AwardCategory>();
            this.ParticipatedMovies = new HashSet<Movie>();
        }

        public virtual ICollection<AwardCategory> Awards { get; set; }

        public virtual ICollection<Movie> ParticipatedMovies { get; set; }
    }
}
