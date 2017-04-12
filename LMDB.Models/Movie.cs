using System;

namespace LMDB.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime DateReleased { get; set; }
    }
}
