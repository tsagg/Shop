using Microsoft.AspNet.Identity;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    /// <summary>
    /// Контроллер для работы с корзиной
    /// </summary>
    public class CartController : Controller
    {
        //Контекст
        ApplicationContext db = new ApplicationContext();

        /// <summary>
        /// Добавление в корзину
        /// </summary>
        /// <param name="Id">Айди продукта</param>
        /// <returns>Страница каталога</returns>
        public RedirectToRouteResult AddToCart(int Id)
        {
            //Ищем продукт
            Product product = db.Products
                .FirstOrDefault(p => p.Id == Id);

            //Если нашли
            if (product != null)
            {
                //Добавляем в корзину
                GetCart().AddItem(product, 1);
            }
            return RedirectToAction("Shop", "Home");
        }

        /// <summary>
        /// Удаление продукта из корзины
        /// </summary>
        /// <param name="Id">Айди продукта</param>
        /// <returns>Страница корзины</returns>
        public RedirectToRouteResult RemoveFromCart(int Id)
        {
            //Ищем продукт
            Product product = db.Products
                .FirstOrDefault(p => p.Id == Id);

            if (product != null)
            {
                //Удаляем
                GetCart().RemoveLine(product);
            }
            return RedirectToAction("Cart", "Cart");
        }

        /// <summary>
        /// Получение корзины из сессии
        /// </summary>
        /// <returns>Корзина</returns>
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

        /// <summary>
        /// Отображение страницы корзины
        /// </summary>
        /// <returns>Страница корзины</returns>
        [Authorize]
        public ActionResult Cart()
        {
            //Передаем полученную из сессии корзину в представление
            return View(new CartIndexViewModel
            {
                Cart = GetCart()
            });
        }

        /// <summary>
        /// GET метод оформления заказа
        /// </summary>
        /// <returns>Страница оформления заказа</returns>
        [Authorize]
        [HttpGet]
        public ActionResult MakeOrder()
        {
            return View();
        }

        /// <summary>
        /// POST метод оформления заказа
        /// </summary>
        /// <param name="model">Модель оформления заказа</param>
        /// <param name="cart">Корзина</param>
        /// <returns>Запись в БД и переадресация на страницу завершения оформления заказа, если нет ошибок, иначе текст ошибок</returns>
        [Authorize]
        [HttpPost]
        public ActionResult MakeOrder(OrderModel model, Cart cart)
        {
            //Получаем корзину и проверяем не пустая ли она
            cart = GetCart();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, ваша корзина пуста!");
                return View(model);
            }

            //Проверяем поле "Номер телефона"
            if (String.IsNullOrEmpty(model.UserPhone))
            {
                ModelState.AddModelError("", "Введите номер телефона!");
                return View(model);
            }

            //Строка для записи товаров
            string orderedItems = "";

            //Записываем в строку все товары из корзины и их кол-во
            foreach (var line in cart.Lines)
            {
                orderedItems += line.Product.Id.ToString() + ":" + line.Quantity.ToString() + ",";
            }

            //Создаем объект заказа
            Order order = new Order
            {
                UserPhone = model.UserPhone,
                UserComment = model.UserComment,
                Date = DateTime.Now.ToString(),
                UserId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                OrderedProducts = orderedItems
            };

            //Записываем в БД
            db.Orders.Add(order);
            db.SaveChanges();

            //Очищаем корзину
            cart.Clear();

            return RedirectToAction("EndOrder", "Cart");
        }

        /// <summary>
        /// Страница успешного оформления заказа
        /// </summary>
        /// <returns>Представление</returns>
        [Authorize]
        public ActionResult EndOrder()
        {
            return View();
        }
    }
}