using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiShop.Models;
using System.Configuration;
using WebConfig.Util;


namespace MultiShop.Areas.Admin.Controllers
{
    public class ManageController : AdminController
    {
        MultiShopDbContext db = new MultiShopDbContext();
        ApplicationDbContext sdb = new ApplicationDbContext();
        // GET: Admin/Manage
        public ActionResult Index()
        {
            ViewBag.Orders = db.Orders.ToList().Count;
            ViewBag.Users = sdb.Users.ToList().Count;
            ViewBag.Product = db.Products.ToList().Count;
            ViewBag.Category = db.Categories.ToList().Count;
            return View();
        }
        public ActionResult Setting()
        {
            var section = ConfigurationManager.AppSettings;

            ViewBag.Setting = section;
            return View("Setting");
        }
        public ActionResult UpdateSetting(string SMTPServer, int Port, string CredentialUserName, string CredentialPassword, bool EnableSsl)
        {
            Config.AddOrUpdateAppSettings("SMTPServer", SMTPServer);
            Config.AddOrUpdateAppSettings("Port", Port.ToString());
            Config.AddOrUpdateAppSettings("CredentialUserName", CredentialUserName);
            Config.AddOrUpdateAppSettings("CredentialPassword", StringCipher.Encrypt(CredentialPassword, "c1bc0bgq"));
            Config.AddOrUpdateAppSettings("EnableSsl", EnableSsl.ToString());
            var section = ConfigurationManager.AppSettings;

            ViewBag.Setting = section;
            return View("Setting");
        }
    }
}