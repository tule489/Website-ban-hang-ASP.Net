using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDoGiaDung.Models;

namespace WebsiteBanDoGiaDung.Controllers
{
    public class LayoutController : Controller
    {
        // GET: Layout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListProduct(int categoryID)
        {
            BanDoGiaDungEntities db = new BanDoGiaDungEntities();
            List<Product> lst = db.Products.Where(x => x.categoryID == categoryID).ToList();
            Category category = db.Categories.Where(x => x.id == categoryID).FirstOrDefault();
            ViewBag.categoryName = category.name;
            ViewBag.categoryID = category.id;
            return PartialView(lst);
        }
    }
}