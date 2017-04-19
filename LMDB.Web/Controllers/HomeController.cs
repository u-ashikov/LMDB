namespace LMDB.Web.Controllers
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new MoviesContext();
            var randomPosters = new List<string>();

            context.Movies.Load();
            var movies = context.Movies.Local;
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    //var index = random.Next(0, movies.Count - 1);
                    var poster = "data:image/jpeg;base64," + Convert.ToBase64String(movies[i].Poster);
                    randomPosters.Add(poster);
                }
            }
            catch (Exception) { ViewBag.Posters = randomPosters; }

            ViewBag.Posters = randomPosters;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}