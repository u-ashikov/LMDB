namespace LMDB.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public Country()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.MovieContributors = new HashSet<Contributor>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Contributor> MovieContributors { get; set; }
    }
}
