using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Controllers
{
    public class MailController : BaseController
    {
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Send(string name, string email, string message)

        {
            try
            {
                var from = name + "<" + email + ">";
                XMail.Send(from, "helmetsshops@gmail.com", "Yêu cầu hỗ trợ", message);
            }
            catch
            {

            }
            return View();

        }
    }
}
