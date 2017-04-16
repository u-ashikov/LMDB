namespace LMDB.ViewModels.Movie
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class MovieEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime DateReleased { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        public string Genres { get; set; }

        [Required]
        public string Actors { get; set; }

        public HttpPostedFileBase MoviePoster { get; set; }
    }
}
