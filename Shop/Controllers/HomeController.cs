using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;

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

        [Authorize]
        public ActionResult Shop()
        {
            ApplicationContext db = new ApplicationContext();
            List<Product> products = db.Products.ToList();
            return View(products);
        }
    }
}