namespace LMDB.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class MovieCreateViewModel
    {
        [Index("MovieTitleIndex",IsUnique = true)]
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
