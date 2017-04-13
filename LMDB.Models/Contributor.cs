namespace LMDB.Models
{
    using Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Contributor
    {
        public Contributor()
        {
            this.Awards = new HashSet<AwardCategory>();
            this.Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "First name must contain only letters with maximum length 50!")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]{1,50}$", ErrorMessage = "Last name must contain only letters with maximum length 50!")]
        public string LastName { get; set; }

        [Required]
        [StringLength(2500)]
        public string Biography { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public DateTime? Birthdate { get; set; }

        public byte[] Picture { get; set; }

        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<AwardCategory> Awards { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
