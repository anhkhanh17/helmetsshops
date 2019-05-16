using MultiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Controllers
{
    public class ValidatingController : BaseController
    {
        protected ApplicationDbContext sdb = new ApplicationDbContext();
        public JsonResult IsUserExists(string username)   
            {
                var model = new { valid = !sdb.Users.Any(x => x.UserName == username) };
                return Json(model, JsonRequestBehavior.AllowGet);
	        //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
	       
	        }

        public JsonResult IsRoleExists(string NewRoleName)
        {
            var model = new { valid = !sdb.Roles.Any(x => x.Name == NewRoleName) };
            return Json(model, JsonRequestBehavior.AllowGet);
            

        }
        public JsonResult IsSupplierExists(string Id)
        {
            var model = new { valid = !db.Suppliers.Any(x => x.Id == Id) };
            return Json(model, JsonRequestBehavior.AllowGet);


        }  

    }
}