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

    public class MovieController : Controller
    {
        private MoviesContext db = new MoviesContext();

        // GET: Movie
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Director).Include(m => m.Review);
            var moviesViewModels = Mapper.Map<List<MovieIndexViewModel>>(movies);
            return View(moviesViewModels);
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
            return View(movieDetails);
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "FirstName");
            ViewBag.Id = new SelectList(db.Reviews, "ReviewedMovieId", "Content");
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,DateReleased,DirectorId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "FirstName", movie.DirectorId);
            ViewBag.Id = new SelectList(db.Reviews, "ReviewedMovieId", "Content", movie.Id);
            return View(movie);
        }

        // GET: Movie/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "FirstName", movie.DirectorId);
            ViewBag.Id = new SelectList(db.Reviews, "ReviewedMovieId", "Content", movie.Id);
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,DateReleased,DirectorId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DirectorId = new SelectList(db.Directors, "Id", "FirstName", movie.DirectorId);
            ViewBag.Id = new SelectList(db.Reviews, "ReviewedMovieId", "Content", movie.Id);
            return View(movie);
        }

        // GET: Movie/Delete/5
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
