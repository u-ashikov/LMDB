namespace LMDB.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
