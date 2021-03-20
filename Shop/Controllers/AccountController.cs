using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Teacher"))
                    return RedirectToAction("Teacher", "Home");
                else
                    return RedirectToAction("Student", "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model, bool check)
        {
            if (!check)
            {
                ModelState.AddModelError("", "Вы не дали согласие на обработку персональных данных");
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, MiddleName = model.MiddleName };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Account", "Home");
            }
            else
            {
                ViewBag.returnUrl = returnUrl;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Account", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home", null);
        }

        [Authorize]
        public async Task<ActionResult> Edit()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                EditModel model = new EditModel { FirstName = user.FirstName, LastName = user.LastName, MiddleName = user.MiddleName };
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(EditModel model)
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Account", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Что-то пошло не так");
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed()
        {
            ApplicationUser user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Logout", "Account");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}