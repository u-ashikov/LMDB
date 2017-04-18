namespace LMDB.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using LMDB.Data;
    using LMDB.Models;
    using AutoMapper;
    using ViewModels.Comment;

    public class CommentsController : Controller
    {
        private MoviesContext db = new MoviesContext();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.Comments.Include(c => c.Author).Include(c => c.CommentedMovie);
            return View(comments.ToList());
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            //ViewBag.AuthorId = new SelectList(db.ApplicationUsers, "Id", "FirstName");
            ViewBag.CommentedMovieId = new SelectList(db.Movies, "Id", "Title");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,AuthorId,CommentedMovieId,Date")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.AuthorId = new SelectList(db.ApplicationUsers, "Id", "FirstName", comment.AuthorId);
            ViewBag.CommentedMovieId = new SelectList(db.Movies, "Id", "Title", comment.CommentedMovieId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            var commentEditView = Mapper.Instance.Map<CommentEditViewModel>(comment);
       
            return View("Edit",commentEditView);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(CommentEditViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var editedComment = db.Comments.Find(comment.Id);

                editedComment.Content = comment.Content;
                editedComment.Date = comment.Date;

                db.Entry(editedComment).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index","Movie");
            }
            
            return View(comment);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
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
