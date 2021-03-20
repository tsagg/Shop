using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.User.Identity.GetUserId() != null)
                return RedirectToAction("Account", "Home");

            return View();
        }

        [Authorize]
        public ActionResult Account()
        {
            return View();
        }
    }
}