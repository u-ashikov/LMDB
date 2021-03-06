﻿namespace LMDB.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    public class CommentCreateViewModel
    {
        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        public int CommentedMovieId { get; set; }
    }
}