using MultiShop.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebConfig.Util;
using System.Net;
using System.Web.Helpers;

namespace MultiShop.Areas.Admin.Controllers
{
    public class MasterController : SecurityController
    {
        public ActionResult Index()
        {
            ViewBag.Users = sdb.Users.ToList(); //.Where(u => u.Roles.Count > 0);
            ViewBag.Roles = sdb.Roles.ToList();

            ViewBag.OldRoleName = new SelectList(sdb.Roles, "Name", "Name");
            return View();
        }

        public ActionResult AddRole(String NewRoleName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var role = new IdentityRole();
                    role.Name = NewRoleName;
                    sdb.Roles.Add(role);
                    sdb.SaveChanges();
                    Success(string.Format("<b>{0}</b> Thêm thành công", NewRoleName), true);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error" + e.InnerException);
                Danger(string.Format("<b>{0}</b> Thêm thất bại", e.Message + e.InnerException), true);
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RemoveRole(String OldRoleName)
        {
            try
            {
                var role = sdb.Roles.Single(r => r.Name == OldRoleName);
                sdb.Roles.Remove(role);
                sdb.SaveChanges();
                Success(string.Format("<b>{0}</b> Xóa thành công", OldRoleName), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error" + e.InnerException);
                Danger(string.Format("<b>{0}</b> Xóa thất bại", e.Message + e.InnerException), true);
            }

            return RedirectToAction("Index");
        }

        /*
         * USER
         */
        //public async Task<ActionResult> AddUser(String UserName, String Password, String[] Roles)
        //{
        //    var user = new ApplicationUser();
        //    user.UserName = UserName;

        //    var result = await UserManager.CreateAsync(user, Password);

        //    if (result.Succeeded)
        //    {
        //        sdb.SaveChanges();
        //        foreach (var role in Roles)
        //        {
        //            await UserManager.AddToRoleAsync(user.Id, role);
        //            sdb.SaveChanges();
        //        }
        //    }

        //    return RedirectToAction("/");
        //}
        public ActionResult AddUser(String UserName, String Password, String Email, String Fullname, String[] UserRoles)
        {
            MultiShopDbContext db = new MultiShopDbContext();
            var user = new ApplicationUser();
            user.UserName = UserName;


            var result = UserManager.Create(user, Password);


            if (result.Succeeded)
            {
                // create info as customer
                try
                {
                    Customer cus = new Customer();
                    cus.Id = UserName;
                    cus.Email = Email;
                    cus.Password = StringCipher.Encrypt(Password, "c1bc0bgq");
                    cus.Fullname = Fullname;
                    cus.Activated = false;

                    db.Customers.Add(cus);
                    db.SaveChanges();
                    Success(string.Format("<b>Tài khoản {0}</b> tạo thành công", UserName), true);
                }
                catch
                {

                }



                try
                {
                    if (UserRoles.Length > 0 || UserRoles != null)
                    {
                        foreach (var role in UserRoles)
                        {
                            UserManager.AddToRole(user.Id, role);
                        }
                    }



                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Error" + e.InnerException);
                    Danger(string.Format("<b>{0}</b> Tạo thất bại", e.Message + e.InnerException), true);
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveUser(String UserName)
        {
            try
            {
                var user = UserManager.FindByName(UserName);


                foreach (var ur in user.Roles.ToList())
                {

                    UserManager.RemoveFromRole(user.Id, ur.RoleId);
                }

                sdb.Users.Remove(user);

                sdb.SaveChanges();
                Success(string.Format("<b>Tải khoản {0}</b> đã xóa thành công", UserName), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error" + e.InnerException);
                Danger(string.Format("<b>{0}</b> Xóa thất bại", e.Message + e.InnerException), true);
            }

            return RedirectToAction("Index");
        }


        public ActionResult ResetPassword(string Id)
        {
            var user = UserManager.FindById(Id);
            ViewBag.UserName = user.UserName;
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(string Id, string Password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = UserManager.FindById(Id);
                    ViewBag.UserName = user.UserName;
                    UserManager.RemovePassword(Id);
                    UserManager.AddPassword(Id, Password);
                    Success(string.Format("<b>Tài khoản {0}</b> đổi mật khẩu" + Password + " thành công.", user.UserName), true);
                    return RedirectToAction("Index", "Master");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Error" + e.InnerException);
                Danger(string.Format("<b>{0}</b> Đổi mật khẩu thất bại", e.Message + e.InnerException), true);
            }
            return View();
        }

        public ActionResult UpdateRole(String Name, bool Status, String UserName)
        {
            var user = UserManager.FindByName(UserName);
            if (Status == true)
            {
                UserManager.AddToRole(user.Id, Name);
            }
            else
            {
                UserManager.RemoveFromRole(user.Id, Name);
            }
            return Content("Cập nhật thành công!");
        }
        public ActionResult Info(string  id, Customer customer)
        {
            MultiShopDbContext db = new MultiShopDbContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer cs = db.Customers.Find(id);
            //cs.Id = Crypto.VerifyHashedPassword(cs.Id, id);
            if(cs == null)
            {
                return HttpNotFound();
            }
            return View(cs);
        }

    }
}