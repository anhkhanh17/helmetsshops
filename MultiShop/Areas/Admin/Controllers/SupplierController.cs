using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiShop.Models;

namespace MultiShop.Areas.Admin.Controllers
{
    public class SupplierController : AdminController
    {
        MultiShopDbContext db = new MultiShopDbContext();

        public ActionResult Index()
        {
            var model = new Supplier();

            ViewBag.Suppliers = db.Suppliers.ToList();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Insert(Supplier model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    db.Suppliers.Add(model);
                    db.SaveChanges();
                    ViewBag.Suppliers = db.Suppliers.ToList();
                    Success(string.Format("<b>{0}</b> Thêm thành công", model.Name), true);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Thêm thất bại", e.Message + e.InnerException), true);
                return RedirectToAction("Index");

            }
            // return View("Index");
            return View("Index", model);
        }

        public ActionResult Edit([Bind(Exclude = "Id")]string Id)
        {
            // var model = db.Suppliers.Find(Id);
            var model = db.Suppliers.Single(p => p.Id == Id);
            ViewBag.Suppliers = db.Suppliers.ToList();
            return View("Index", model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Supplier model)
        {
            try
            {
                var newModel = db.Suppliers.Single(p => p.Id == model.Id);
                newModel.Id = model.Id;
                newModel.Name = model.Name;
                newModel.Email = model.Email;
                newModel.Phone = model.Phone;
                newModel.Logo = model.Logo;
                db.SaveChanges();
                ViewBag.Suppliers = db.Suppliers.ToList();
                Success(string.Format("<b>{0}</b> Cập nhật thành công.", model.Name), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Cập nhật thất bại.", e.Message + e.InnerException), true);
                return RedirectToAction("Index");
            }
            return View("Index", model);
            // return RedirectToAction("Index");
        }

        public ActionResult Delete(String Id)
        {
            try
            {
                var model = db.Suppliers.Single(p => p.Id == Id);
                db.Suppliers.Remove(model);
                db.SaveChanges();
                Success(string.Format("<b>{0}</b> Xóa thành công.", model.Name), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Xóa thất bại.", e.Message + e.InnerException), true);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public ActionResult DeleteSelected(string[] ids)
        {
            try
            {


                if (ids == null)
                {

                    ModelState.AddModelError("", "Chưa có mục nào được chọn để xóa !");

                }


                foreach (var item in ids)
                {
                    var sp = db.Suppliers.Single(p => p.Id == item);
                    db.Suppliers.Remove(sp);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Xóa thất bại.", e.Message + e.InnerException), true);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}