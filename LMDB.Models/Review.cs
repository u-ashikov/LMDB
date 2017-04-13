namespace LMDB.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public int ReviewedMovieId { get; set; }

        [Required]
        [StringLength(5000)]
        public string Content { get; set; }

        public DateTime DatePublished { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Movie ReviewedMovie { get; set; }
    }
}
