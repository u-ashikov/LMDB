using System.Web.Mvc;

namespace LMDB.Web.Controllers
{
    public class MovieController : Controller
    {
        public ActionResult AllMovies()
        {
            ViewBag.Message = "All movies in one place";

            return View();
        }
    }
}