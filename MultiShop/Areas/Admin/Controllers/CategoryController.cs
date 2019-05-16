using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiShop.Models;

namespace MultiShop.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        MultiShopDbContext db = new MultiShopDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var model = new Category();
            ViewBag.Category = db.Categories.ToList();
            return View(model);
            
        }
        public ActionResult Insert([Bind(Exclude = "Id")]Category model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(model);
                    db.SaveChanges();
                    ViewBag.Category = db.Categories.ToList();
                   Success( string.Format("<b>{0}</b> Thêm thành công", model.Name),true);

                }

            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Thêm thất bại", e.Message + e.InnerException), true);
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }

        public ActionResult Edit(int Id)
        {
            var model = db.Categories.Single(p => p.Id == Id);
            ViewBag.Category = db.Categories.ToList();
            return View("Index", model);
        }
        public ActionResult Update(Category model)
        {
            try
            {
                var newmodel = db.Categories.Single(p => p.Id == model.Id);
                newmodel.Id = model.Id;
                newmodel.Name = model.Name;
                newmodel.Image = model.Image;
                newmodel.Icon = model.Icon;
                db.SaveChanges();
                ViewBag.Category = db.Categories.ToList();
                Success(string.Format("<b>{0}</b> Cập nhật thành công", model.Name), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Cập nhật thất bại", e.Message + e.InnerException), true);
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }


        public ActionResult Delete(int Id)
        {
            try
            {
                var model = db.Categories.Single(p => p.Id == Id);
                db.Categories.Remove(model);
                db.SaveChanges();
                Success(string.Format("<b>{0}</b> Xóa thành công", model.Name), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Xóa thất bại", e.Message + e.InnerException), true);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        public ActionResult DeleteSelected(int[] ids)
        {
            try
            {

                var items = "";
                if (ids.ToString() == null)
                {

                    ModelState.AddModelError("", "Không có mục nào được chọn để xóa!");

                }


                foreach (var item in ids)
                {
                    var sp = db.Categories.Single(p => p.Id == item);
                    items += sp.Name + ", ";
                    db.Categories.Remove(sp);
                    db.SaveChanges();
                }
                Success(string.Format("<b>{0}</b> Xóa thành công", items), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "error: " + e.InnerException);
                Danger(string.Format("<b>{0}</b> Xóa thất bại", e.Message + e.InnerException), true);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


    }
}