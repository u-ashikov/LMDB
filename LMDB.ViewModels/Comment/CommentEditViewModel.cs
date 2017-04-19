namespace LMDB.ViewModels.Comment
{
    using System.ComponentModel.DataAnnotations;

    public class CommentEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }
    }
}
