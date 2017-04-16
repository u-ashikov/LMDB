namespace LMDB.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;

    public class MovieIndexViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string DirectorName { get; set; }

        public DateTime DateReleased { get; set; }

        public double Rating { get; set; }

        public List<string> Genres { get; set; } = new List<string>();

        public List<string> Actors { get; set; } = new List<string>();

        public int Count { get; set; }
    }
}
