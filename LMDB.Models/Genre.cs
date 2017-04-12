namespace LMDB.Models
{
    using System.Collections.Generic;

    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
