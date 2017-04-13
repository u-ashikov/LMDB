namespace LMDB.Models
{
    using System;
    using System.Collections.Generic;
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime DateReleased { get; set; }

        public int DirectorId  { get; set; }

        public virtual Director Director { get; set; }

        public virtual Review Review { get; set; }

        public virtual ICollection<Actor> Actors { get; set; } = new HashSet<Actor>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<AwardCategory> Awards { get; set; } = new HashSet<AwardCategory>();

        public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

        public virtual ICollection<ApplicationUser> Likes { get; set; } = new HashSet<ApplicationUser>();

        public virtual ICollection<ApplicationUser> Dislikes { get; set; } = new HashSet<ApplicationUser>();

        public virtual ICollection<ApplicationUser> MovieFans { get; set; } = new HashSet<ApplicationUser>();      
    }
}
