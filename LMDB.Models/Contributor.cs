namespace LMDB.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;

    public class Contributor
    {
        public Contributor()
        {
            this.Awards = new HashSet<AwardCategory>();
            this.Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthdate { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<AwardCategory> Awards { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
