namespace LMDB.ViewModels.Movie
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class MovieCreateViewModel
    {
        public string Title { get; set; }

        public DateTime DateReleased { get; set; }

        public string Director { get; set; }

        public string Genres { get; set; }

        public string Actors { get; set; }

        public HttpPostedFileBase MoviePoster { get; set; }
    }
}
