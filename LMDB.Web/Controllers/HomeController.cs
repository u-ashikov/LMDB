using LMDB.ViewModels.Movie;

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
            var random = new Random();
            var context = new MoviesContext();
            var randomPosters = new List<string>();
            string poster = string.Empty;

            context.Movies.Load();
            var movies = AutoMapper.Mapper.Instance.Map<List<MovieIndexViewModel>>(context.Movies.Local);
            try
            {
                for (int i = 0; i < 8; i++)
                {
                    var index = random.Next(0, movies.Count);
                    var movie = movies[index];
                    if (movie.Poster == null)
                    {
                        poster = movie.PosterFromFolder;
                    }
                    else
                    {
                        poster = movie.Poster;
                    }

                    randomPosters.Add(poster);
                }
            }
            catch (Exception) { ViewBag.Posters = randomPosters; }

            ViewBag.Posters = randomPosters;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}