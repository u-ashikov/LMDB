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
    using Microsoft.AspNet.Identity;

    public class CommentsController : Controller
    {
        private MoviesContext db = new MoviesContext();

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        public ActionResult CreateComment(CommentCreateViewModel commentViewModel)
        {
            if (ModelState.IsValid)
            {
                var comment = Mapper.Instance.Map<Comment>(commentViewModel);

                var userId = User.Identity.GetUserId();
                var movie = db.Movies.Find(comment.CommentedMovieId);

                comment.AuthorId = userId;
                comment.Date = DateTime.Now;

                movie.Comments.Add(comment);

                db.SaveChanges();
                return RedirectToAction("Details","Movie",new { id = commentViewModel.CommentedMovieId });
            }

            return RedirectToAction("Details","Movie",new { id = commentViewModel.CommentedMovieId });
        }

        // GET: Comments/Edit/5
        [Authorize]
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

            if ((User.Identity.IsAuthenticated && comment.AuthorId == User.Identity.GetUserId()) || User.IsInRole("Admin"))
            {
                var commentEditView = Mapper.Instance.Map<CommentEditViewModel>(comment);

                return View("Edit", commentEditView);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(CommentEditViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var editedComment = db.Comments.Find(comment.Id);

                editedComment.Content = comment.Content;

                db.Entry(editedComment).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Details","Movie",new { id=editedComment.CommentedMovieId});
            }

            return RedirectToAction("EditComment", new {id = comment.Id });
        }

        // GET: Comments/Delete/5
        [Authorize]
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

            if ((User.Identity.IsAuthenticated && comment.AuthorId == User.Identity.GetUserId()) || User.IsInRole("Admin"))
            {
                return View(comment);
            }
            return RedirectToAction("Login", "Account");           
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Movie", new {id = comment.CommentedMovieId});
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
