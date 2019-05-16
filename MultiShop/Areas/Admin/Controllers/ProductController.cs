using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.Admin.Controllers
{
    public class ProductController : AdminController
    {
        MultiShopDbContext db = new MultiShopDbContext();
        // GET: Admin/Product
        public ActionResult Index()
        {

            var model = db.Products.ToList();
            return View(model);
        }

        public int getLastProduct()
        {

            var LastId = 0;
            try
            {
                LastId = db.Products
                .OrderByDescending(q => q.Id)
                .Select(q => q.Id)
                .First();
            }
            catch
            {

            }

            return LastId + 1;

        }
        public ActionResult Insert()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");



            ViewBag.LastId = getLastProduct();

            return View();
        }
        //[Bind(Exclude = "Id")]

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Insert(Product model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    //var f = Request.Files["uplImage"];
                    //if (f != null && f.ContentLength > 0)
                    //{
                    //    model.Image = model.Id
                    //     + f.FileName.Substring(f.FileName.LastIndexOf("."));
                    //    f.SaveAs(Server.MapPath("/Content/img/products/" + model.Image));

                    //}

                    var filename = Request["Image"];
                    var tmp = filename.Substring(filename.LastIndexOf("/"));
                    tmp = tmp.Replace("/", "");
                    model.Image = tmp;
                    db.Products.Add(model);
                    db.SaveChanges();
                    Success(string.Format("<b>{0}</b> Thêm thành công", model.Name), true);
                    return RedirectToAction("Index");


                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.InnerException);
                Danger(string.Format("<b>{0}</b> Thêm thất bại", e.Message + e.InnerException), false);

            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", model.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", model.SupplierId);
            ViewBag.LastId = getLastProduct();
            return View();
            //return RedirectToAction("Insert");
        }

        [ValidateInput(false)]
        public ActionResult Edit(int Id)
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            var model = db.Products.Single(p => p.Id == Id);

            return View(model);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Product model)
        {
            try
            {
                var newModel = db.Products.Single(p => p.Id == model.Id);

                //  var filename = Request["Image"];
                //  var tmp = filename.Substring(filename.LastIndexOf("/"));
                //  tmp = tmp.Replace("/", "");
                //  model.Image = tmp;
                //  newModel.Image = model.Image.Substring(model.Image.LastIndexOf("/"));
                //   newModel.Image = newModel.Image.Replace("/","");
                if (model.Image.Contains("/"))
                {
                    newModel.Image = model.Image.Substring(model.Image.LastIndexOf("/"));
                    newModel.Image = newModel.Image.Replace("/", "");

                }
                else
                {
                    newModel.Image = model.Image;
                }


                newModel.Id = model.Id;
                newModel.Name = model.Name;
                newModel.Latest = model.Latest;
                newModel.Special = model.Special;
                newModel.Special = model.Special;
                newModel.ProductDate = model.ProductDate;
                newModel.SupplierId = model.SupplierId;
                newModel.UnitBrief = model.UnitBrief;
                newModel.UnitPrice = model.UnitPrice;
                newModel.Views = model.Views;
                newModel.Description = model.Description;
                newModel.Discount = model.Discount;
                db.SaveChanges();

                Success(string.Format("<b>{0}</b> Cập nhật thành công", model.Name), true);
                return RedirectToAction("Index", "Product");
            }
            catch (Exception e)
            {
                // ModelState.AddModelError("", e.InnerException);
                Danger(string.Format("<b>{0}</b> Cập nhật thất bại", e.Message + e.InnerException), true);

            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", model.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", model.SupplierId);
            return View("Edit", model);
            // return RedirectToAction("Index");
        }

        public ActionResult Delete(int Id)
        {
            try
            {
                var model = db.Products.Single(p => p.Id == Id);
                db.Products.Remove(model);
                db.SaveChanges();
                Success(string.Format("<b>{0}</b> was successfully delete.", model.Name), true);
            }
            catch
            {

            }

            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var model = db.Products.Find(id);
            return View("Detail", model);
        }


        public ActionResult DeleteSelected(int[] ids)
        {
            try
            {

                var items = "";
                if (ids == null)
                {

                    ModelState.AddModelError("", "None of the reconds has been selected for delete action !");

                }


                foreach (var item in ids)
                {



                    var sp = db.Products.Single(p => p.Id == item);
                    items += sp.Name + ", ";
                    db.Products.Remove(sp);
                    db.SaveChanges();

                }
                Success(string.Format("<b>{0}</b> was successfully delete.", items), true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.InnerException);
                Danger(string.Format("<b>{0}</b> Insert Error.", e.Message + e.InnerException), true);
            }
            return RedirectToAction("Index");
        }


    }
}