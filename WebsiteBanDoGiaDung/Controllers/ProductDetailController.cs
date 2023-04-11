using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDoGiaDung.Models;

namespace WebsiteBanDoGiaDung.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        public ActionResult Index(int productID)
        {
            BanDoGiaDungEntities db = new BanDoGiaDungEntities();
            Product obj = db.Products.Where(x => x.id == productID).FirstOrDefault();
            return View(obj);
        }
    }
}