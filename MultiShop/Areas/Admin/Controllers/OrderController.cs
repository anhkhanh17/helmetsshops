using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        MultiShopDbContext db = new MultiShopDbContext();
        public ActionResult Index()
        {
            var orders = db.Orders.ToList();

            return View(orders);
        }

        public ActionResult Detail(int id)
        {
            var order = db.Orders.Find(id);
            ViewBag.Total = order.Amount;
            return View(order);
        }


    }
}