using Microsoft.AspNet.Identity;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class CartController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        public RedirectToRouteResult AddToCart(int Id)
        {
            Product product = db.Products
                .FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Shop", "Home");
        }

        public RedirectToRouteResult RemoveFromCart(int Id)
        {
            Product product = db.Products
                .FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Cart", "Cart");
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        [Authorize]
        public ActionResult Cart()
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart()
            });
        }

        [Authorize]
        [HttpGet]
        public ActionResult MakeOrder()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult MakeOrder(OrderModel model, Cart cart)
        {
            cart = GetCart();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
                return View(model);
            }

            if (String.IsNullOrEmpty(model.UserPhone))
            {
                ModelState.AddModelError("", "Введите номер телефона!");
                return View(model);
            }

            string orderedItems = "";

            foreach (var line in cart.Lines)
            {
                orderedItems += line.Product.Id.ToString() + ":" + line.Quantity.ToString() + ",";
            }

            Order order = new Order
            {
                UserPhone = model.UserPhone,
                UserComment = model.UserComment,
                Date = DateTime.Now.ToString(),
                UserId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                OrderedProducts = orderedItems
            };

            db.Orders.Add(order);
            db.SaveChanges();

            cart.Clear();

            return RedirectToAction("EndOrder", "Cart");
        }

        [Authorize]
        public ActionResult EndOrder()
        {
            return View();
        }
    }
}