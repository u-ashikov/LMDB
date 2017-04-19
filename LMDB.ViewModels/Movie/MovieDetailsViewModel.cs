namespace LMDB.ViewModels.Movie
{
    using System.Collections.Generic;
    using LMDB.Models;

    public class MovieDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string Director { get; set; }

        public string Review { get; set; }

        public string Poster { get; set; }

        public ICollection<Actor> Actors { get; set; }

        public ICollection<string> Genres { get; set; }

        public ICollection<string> Awards { get; set; }

        public ICollection<string> Likes { get; set; }

        public ICollection<string> Dislikes { get; set; }

        public ICollection<string> Fans { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
