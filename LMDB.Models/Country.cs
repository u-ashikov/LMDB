namespace LMDB.Models
{
    using System.Collections.Generic;

    public class Country
    {
        public Country()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.MovieContributors = new HashSet<Contributor>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Contributor> MovieContributors { get; set; }
    }
}
