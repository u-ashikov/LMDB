namespace LMDB.Models
{
    using System;

    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public int CommentedMovieId { get; set; }

        public DateTime Date { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public virtual Movie CommentedMovie { get; set; }
    }
}
