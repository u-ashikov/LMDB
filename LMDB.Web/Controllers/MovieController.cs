namespace LMDB.Web.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Net;
    using System.Web.Mvc;
    using LMDB.Data;
    using LMDB.Models;
    using LMDB.ViewModels.Movie;
    using AutoMapper;
    using System.IO;
    using System.Web;
    using System;
    using System.Linq;

    public class MovieController : Controller
    {
        private MoviesContext db = new MoviesContext();

        // GET: Movie
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Director).Include(m => m.Genres);
            var moviesViewModels = Mapper.Map<List<MovieIndexViewModel>>(movies);
            var genres = db.Genres.ToList();
            var model = Tuple.Create(moviesViewModels, genres);
            return View(model);
        }

        public ActionResult CategoryIndex(string genreName)
        {
            var movies = db.Movies.Include(m => m.Director).Include(m => m.Genres)
                .Where(m => m.Genres.Any(g => g.Name == genreName)).ToList();
            var moviesViewModels = Mapper.Map<List<MovieIndexViewModel>>(movies);
            var genres = db.Genres.ToList();
            var model = Tuple.Create(moviesViewModels, genres);
            return View(model);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = db.Movies.Find(id);
            var movieDetails = Mapper.Map<MovieDetailsViewModel>(movie);

            if (movie == null)
            {
                return HttpNotFound();
            }

            if (movie.Poster != null)
            {
                movieDetails.Poster = "data:image/jpeg;base64," + Convert.ToBase64String(movie.Poster);
            }
            

            return View(movieDetails);
        }

        // GET: Movie/Create

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MovieCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie()
                {
                    Title = model.Title,
                    DateReleased = model.DateReleased,
                    Poster = GetBytesFromFile(model.MoviePoster)
                };

                var reviewAuthor = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                movie.Review =  new Review()
                {
                    Content = model.Review,
                    DatePublished = DateTime.Now,
                    AuthorId = reviewAuthor.Id
                };

                var author = User.Identity.Name;

                var director = GetDirectorByName(db, model.Director);

                if (director == null)
                {
                    AddDirector(db, model.Director);
                }

                movie.DirectorId = GetDirectorByName(db, model.Director).Id;

                var actors = model.Actors.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                foreach (var a in actors)
                {
                    var firstName = a.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    var lastName = a.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    var actor = GetActorByName(db, firstName, lastName);

                    if (actor == null)
                    {
                        movie.Actors.Add(new Actor()
                        {
                            FirstName = firstName,
                            LastName = lastName
                        });
                    }
                    else
                    {
                        movie.Actors.Add(actor);
                    }
                }

                var genres = model.Genres.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var g in genres)
                {
                    var genre = GetGenreByName(db, g);

                    if (genre == null)
                    {
                        movie.Genres.Add(new Genre()
                        {
                            Name = g
                        });
                    }
                    else
                    {
                        movie.Genres.Add(genre);
                    }
                }

                try
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("CreateMovieError", e.Message);
                    return View(model);
                }
                
                return RedirectToAction("Index");
            }

            //ViewBag.DirectorId = new SelectList(db.Directors, "Id", "FirstName", movie.DirectorId);
            //ViewBag.Id = new SelectList(db.Reviews, "ReviewedMovieId", "Content", movie.Id);
            return View(model);
        }

        // GET: Movie/Edit/5

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            var movieEditModel = Mapper.Map<MovieEditViewModel>(movie);

            if (movie == null)
            {
                return HttpNotFound();
            }

            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "FirstName", movie.DirectorId);
            ViewBag.Id = new SelectList(db.Reviews, "ReviewedMovieId", "Content", movie.Id);
            return View(movieEditModel);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieEditViewModel editedMovie)
        {
            if (ModelState.IsValid)
            {
                var movie = db.Movies.Find(editedMovie.Id);

                movie.Title = editedMovie.Title;
                movie.DateReleased = editedMovie.DateReleased;

                if (editedMovie.MoviePoster != null)
                {
                    movie.Poster = GetBytesFromFile(editedMovie.MoviePoster);
                }

                var director = GetDirectorByName(db, editedMovie.Director);

                if (director == null)
                {
                    movie.Director = new Director()
                    {
                        FirstName = editedMovie.Director.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0],
                        LastName = editedMovie.Director.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1],
                    };
                }
                else
                {
                    movie.Director = director;
                }

                var editedGenres = editedMovie.Genres.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var oldGenres = movie.Genres.Select(g=>g.Name).ToList();

                foreach (var g in oldGenres)
                {
                    if (!editedGenres.Contains(g))
                    {
                        var genreToRemove = db.Genres.FirstOrDefault(gen => gen.Name == g);
                        movie.Genres.Remove(genreToRemove);
                    }
                    else
                    {
                        editedGenres.Remove(g);
                    }
                }

                foreach (var g in editedGenres)
                {
                    var genre = GetGenreByName(db, g);

                    if (genre == null)
                    {
                        movie.Genres.Add(new Genre()
                        {
                            Name = g
                        });
                    }
                    else
                    {
                        movie.Genres.Add(genre);
                    }
                }

                var editedActors = editedMovie.Actors
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(a => a.Trim())
                    .ToList();

                var oldActors = movie.Actors.
                    Select(a => $"{a.FirstName} {a.LastName}")
                    .ToList();

                foreach (var a in oldActors)
                {
                    if (!editedActors.Contains(a))
                    {
                        var firstName = a.Split(' ')[0];
                        var lastName = a.Split(' ')[1];

                        var actorToRemove = db.Actors.FirstOrDefault(act => act.FirstName == firstName && act.LastName == lastName);

                        movie.Actors.Remove(actorToRemove);
                    }
                    else
                    {
                        editedActors.Remove(a);
                    }
                }

                foreach (var a in editedActors)
                {
                    var firstName = a.Split(' ')[0];
                    var lastName = a.Split(' ')[1];

                    var actor = GetActorByName(db, firstName, lastName);

                    if (actor == null)
                    {
                        movie.Actors.Add(new Actor()
                        {
                            FirstName = firstName,
                            LastName = lastName
                        });
                    }
                    else
                    {
                        movie.Actors.Add(actor);
                    }
                }

                movie.Review.Content = editedMovie.Review;

                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            //ViewBag.DirectorId = new SelectList(db.Directors, "Id", "FirstName", movie.DirectorId);
            //ViewBag.Id = new SelectList(db.Reviews, "ReviewedMovieId", "Content", movie.Id);

            return View(editedMovie);
        }

        // GET: Movie/Delete/5

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);

            movie.Actors.Clear();
            movie.Genres.Clear();

            if (movie.Review !=null)
            {
                db.Reviews.Remove(movie.Review);
            }
            
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Evaluate(int id,string status)
        {
            var movieId = id;

            var movie = db.Movies.Find(id);

            if (movie == null)
            {
                return this.RedirectToAction(
                "Details",
                new
                {
                    id = id
                });
            }

            var user = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            if (status == "Like")
            {
                if (movie.Dislikes.Select(d=>d.Id).Contains(user.Id))
                {
                    movie.Dislikes.Remove(user);
                }
                movie.Likes.Add(user);
            }
            else if (status == "Dislike")
            {
                if (movie.Likes.Select(d => d.Id).Contains(user.Id))
                {
                    movie.Likes.Remove(user);
                }
                movie.Dislikes.Add(user);
            }
            else if (status == "Favourite")
            {
                movie.MovieFans.Add(user);
            }

            db.SaveChanges();

            return RedirectToAction("Index", "Movie");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private static byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();
            file.InputStream.CopyTo(stream);
            byte[] data = stream.ToArray();
            return data;
        }

        private static Director GetDirectorByName(MoviesContext context,string director)
        {
            var firstName = director.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            var lastName = director.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

             return context.Directors.FirstOrDefault(d => d.FirstName == firstName && d.LastName == lastName);
        }

        private static void AddDirector(MoviesContext context, string director)
        {
            var firstName = director.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
            var lastName = director.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

            context.Directors.Add(new Director()
            {
                FirstName = firstName,
                LastName = lastName
            });

            context.SaveChanges();
        }

        private static Actor GetActorByName(MoviesContext context, string firstName,string lastName)
        {
            return context.Actors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName);
        }

        private static Genre GetGenreByName(MoviesContext context, string genre)
        {
            return context.Genres.FirstOrDefault(g => g.Name == genre);
        }
    }
}
