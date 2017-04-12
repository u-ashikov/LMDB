﻿namespace LMDB.Models
{
    using System;

    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        public int AuthorId { get; set; }

        public int ReviewedMovieId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Movie ReviewedMovie { get; set; }
    }
}
