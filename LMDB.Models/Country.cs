namespace LMDB.Models
{
    using System.Collections.Generic;

    public class Country
    {
        public Country()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Contributors = new HashSet<Contributor>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CountryCode { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Contributor> Contributors { get; set; }
    }
}
