namespace LMDB.Models
{
    using System.Collections.Generic;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Director
    {
        public int Id { get; set; }

        public Director()
        {
            this.Awards = new HashSet<AwardCategory>();
            this.DirectedMovies = new HashSet<Movie>();
        }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "First name must contain only letters with maximum length 50!")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[a-zA-Z\.\-]{1,50}$", ErrorMessage = "Last name must contain only letters with maximum length 50!")]
        public string LastName { get; set; }

        public string Biography { get; set; }

        public DateTime? Birthdate { get; set; }

        public byte[] Picture { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<AwardCategory> Awards { get; set; }

        public virtual ICollection<Movie> DirectedMovies { get; set; }
    }
}
