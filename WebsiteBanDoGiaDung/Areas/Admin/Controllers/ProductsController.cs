using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebsiteBanDoGiaDung.Models;

namespace WebsiteBanDoGiaDung.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private BanDoGiaDungEntities db = new BanDoGiaDungEntities();

        // GET: Admin/Products
        public ActionResult Index(int? Category, string Search)
        {
            List<Product> lst;
            if (Category == null && Search == null)
            {
                lst = db.Products.ToList();
            }
            else
            {
                if (Category != null)
                {
                    lst = db.Products.Where(x => x.categoryID == Category).ToList();
                }
                else
                {
                    lst = db.Products.Where(x => x.name.Contains(Search)).ToList();
                }
            }

            ViewBag.Category = new SelectList(db.Categories.ToList(), "id", "name");

            return View(lst);
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.categoryID = new SelectList(db.Categories, "id", "name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,categoryID,material,size,price,numberOfTable,numberOfChair,numberOfShelf,description,image")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BanDoGiaDungEntities db = new BanDoGiaDungEntities();

                    string fileName = "";
                    var f = Request.Files["fileImg"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string[] ext = f.FileName.Split('.');
                        fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + ext[ext.Length - 1];
                        string path = Server.MapPath("~/Assets/Images/Products/" + fileName);
                        f.SaveAs(path);
                    }
                    ViewBag.fileName = fileName;
                    product.image = fileName;
                    db.Products.Add(product);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.ToString();
                }
            }

            ViewBag.categoryID = new SelectList(db.Categories, "id", "name", product.categoryID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryID = new SelectList(db.Categories, "id", "name", product.categoryID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,categoryID,material,size,price,numberOfTable,numberOfChair,numberOfShelf,description,image")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BanDoGiaDungEntities db = new BanDoGiaDungEntities();

                    string fileName = "";
                    var f = Request.Files["fileImg"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string[] ext = f.FileName.Split('.');
                        fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + ext[ext.Length - 1];
                        string path = Server.MapPath("~/Assets/Images/Products/" + fileName);
                        f.SaveAs(path);
                    }
                    product.image = fileName;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.ToString();
                }
                return RedirectToAction("Index");
            }
            ViewBag.categoryID = new SelectList(db.Categories, "id", "name", product.categoryID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
