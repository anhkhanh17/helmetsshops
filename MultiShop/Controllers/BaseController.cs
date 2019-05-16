using MultiShop.Models;
using MultiShop.Utils;

namespace MultiShop.Controllers
{
    public class BaseController : CommonController
    {
        public MultiShopDbContext db = new MultiShopDbContext();
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}