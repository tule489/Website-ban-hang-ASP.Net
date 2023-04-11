using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDoGiaDung.Models;

namespace WebsiteBanDoGiaDung.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            BanDoGiaDungEntities db = new BanDoGiaDungEntities();
            List<Category> lst = db.Categories.ToList();
            return View(lst);
        }
    }
}