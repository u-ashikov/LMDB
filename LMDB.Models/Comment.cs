namespace LMDB.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public int CommentedMovieId { get; set; }

        public DateTime Date { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Movie CommentedMovie { get; set; }
    }
}
