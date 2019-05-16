using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiShop.Models;
using System.Data.Entity;

namespace MultiShop.Controllers
{
    public class OrderController : BaseController
    {
        // GET: /Order/
        
        public ActionResult Checkout()
        {

            //Kiểm tra đăng đăng nhập
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "Account");
            }
            //Kiểm tra giỏ hàng
            if (ShoppingCart.Cart.Count == 0)
            {
                return RedirectToAction("About", "Cart");
            }

            var model = new Order();
            //if(ModelState.IsValid)
            //{ 
            string temp = String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now);
            string temp2 = DateTime.Now.AddDays(3).ToString("dd/MM/yyyy HH:mm:ss ");
            model.CustomerId = User.Identity.Name;
            model.OrderDate = Convert.ToDateTime(temp);
            model.RequireDate = Convert.ToDateTime(temp2);
            model.Amount = ShoppingCart.Cart.Total;
            //}
            return View(model);
        }
        public ActionResult Purchase(Order model)
        {

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            db.Orders.Add(model);

                    var cart = ShoppingCart.Cart;
                    foreach (var p in cart.Items)
                    {
                        var d = new OrderDetail
                        {
                            Order = model,
                            ProductId = p.Id,
                            UnitPrice = p.UnitPrice,
                            Discount = p.Discount,
                            Quantity = p.Quantity
                        };
                ViewBag.ProductDetail = cart.Items;
                db.OrderDetails.Add(d);
                    }
                    db.SaveChanges();
                    Session["Cart"] = null;
                    //Success(string.Format("<b><h5>{0}</h4></b>", "Bạn đã đặt hàng thành công. Chúng tôi sẽ sớm liên hệ cho bạn."), true);
                    return RedirectToAction("Detail", new { id = model.Id });
                //    }
                //    catch(Exception ex)
                //    {
                //        Danger(string.Format("<b><h4>Error: {0}</h4></b>", ex.Message), true);
                //        ModelState.AddModelError("", ex.InnerException);
                //    }

                //}
                //Session["Cart"] = null;
                //return View("Checkout", model);

        }

        public ActionResult Detail(int id)
        {
            var order = db.Orders.Find(id);
            ViewBag.Total = order.Amount;
            return View(order);
        }

        public ActionResult List()
        {
            var orders = db.Orders
                .Where(o => o.CustomerId == User.Identity.Name);
            return View(orders);
        }
        public ActionResult Delete(int Id)
        {
                var del = db.Products.Find(Id);
                db.Products.Remove(del);
                db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id)
        {
            var cart = ShoppingCart.Cart;
            cart.Remove(id);

            var info = new { cart.Count, cart.Total };
            return Json(info, JsonRequestBehavior.AllowGet);
        }
    }
}