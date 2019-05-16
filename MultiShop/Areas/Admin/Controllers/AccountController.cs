using MultiShop.Models;
using MultiShop.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace MultiShop.Areas.Admin.Controllers
{

    public class AccountController : SecurityController
    {
        MultiShopDbContext db = new MultiShopDbContext();
        [Authorize]
        public ActionResult Logoff()
        {

            AuthenticationManager.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Admin");
        }

        public ActionResult Login(String returnUrl)
        {
            if (returnUrl.Contains("/Admin/"))
            {
                Response.Redirect("/Admin/Account/Login?returnUrl=" + returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(String UserName, String Password, string returnUrl)
        {
            var user = await UserManager.FindAsync(UserName, Password);
            if (user != null)
            {
                await SignInAsync(user, false);
                if (returnUrl == null)
                {
                    returnUrl = "/Admin/Master";
                }
                return Redirect(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
            }
            return View();
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        public ActionResult Edit()
        {
            var model = db.Customers.Find(User.Identity.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Customer model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

            return View(model);
        }

        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Mật khẩu của bạn đã được thay đổi."
                : message == ManageMessageId.SetPasswordSuccess ? "Mật khẩu của bạn đã được thiết lập."
                : message == ManageMessageId.RemoveLoginSuccess ? "Đăng nhập bên ngoài đã được gỡ bỏ."
                : message == ManageMessageId.Error ? "Đã xảy ra lỗi!"
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        [AllowAnonymous]
        public ActionResult Forgot()
        {
            return View();
        }
        [AllowAnonymous, HttpPost]
        public ActionResult Forgot(String UserName, String Email)
        {
            try
            {
                var cust = db.Customers
                    .Single(c => c.Id == UserName && c.Email == Email);

                var user = UserManager.FindByName(UserName);

                String TokenCode = Guid.NewGuid().ToString();
                UserManager.RemovePassword(user.Id);
                UserManager.AddPassword(user.Id, TokenCode);
                XMail.Send(Email, "Token Code", TokenCode);

                return View("Reset");
            }
            catch
            {
                ModelState.AddModelError("", "Sai thông tin người dùng!");
                return View();
            }
        }
        [AllowAnonymous, HttpPost]
        public ActionResult Reset(String UserName, String TokenCode, String NewPassword)
        {
            var user = UserManager.FindByName(UserName);
            UserManager.ChangePassword(user.Id, TokenCode, NewPassword);
            return RedirectToAction("Login");
        }
    }
}