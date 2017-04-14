namespace LMDB.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public Country()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Directors = new HashSet<Director>();
            this.Actors = new HashSet<Actor>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Director> Directors { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
    }
}
