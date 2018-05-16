using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage_app.Models;

namespace Garage_app.Controllers
{
    public class purchasesController : Controller
    {
        private garageEntities db = new garageEntities();

        // GET: purchases
        public ActionResult Index()
        {
            var purchases = db.purchases.Include(p => p.category).Include(p => p.company).Include(p => p.pay).Include(p => p.product);
            return View(purchases.ToList());
        }

        // GET: purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase purchase = db.purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: purchases/Create
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.categories, "Category_Id", "Category_Name");
            ViewBag.Company_Id = new SelectList(db.companies, "Company_Id", "Company_Name");
            ViewBag.Pay_Id = new SelectList(db.pays, "pay_Id", "Value");
            ViewBag.Product_Id = new SelectList(db.products, "Product_Id", "Product_Name");
            return View();
        }

        // POST: purchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Purchase_Id,Purchase_Name,Product_Id,Company_Id,Category_Id,Purchase_Date,Quantity,Pay_Id")] purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.purchases.Add(purchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_Id = new SelectList(db.categories, "Category_Id", "Category_Name", purchase.Category_Id);
            ViewBag.Company_Id = new SelectList(db.companies, "Company_Id", "Company_Name", purchase.Company_Id);
            ViewBag.Pay_Id = new SelectList(db.pays, "pay_Id", "Value", purchase.Pay_Id);
            ViewBag.Product_Id = new SelectList(db.products, "Product_Id", "Product_Name", purchase.Product_Id);
            return View(purchase);
        }

        // GET: purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase purchase = db.purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_Id = new SelectList(db.categories, "Category_Id", "Category_Name", purchase.Category_Id);
            ViewBag.Company_Id = new SelectList(db.companies, "Company_Id", "Company_Name", purchase.Company_Id);
            ViewBag.Pay_Id = new SelectList(db.pays, "pay_Id", "Value", purchase.Pay_Id);
            ViewBag.Product_Id = new SelectList(db.products, "Product_Id", "Product_Name", purchase.Product_Id);
            return View(purchase);
        }

        // POST: purchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Purchase_Id,Purchase_Name,Product_Id,Company_Id,Category_Id,Purchase_Date,Quantity,Pay_Id")] purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_Id = new SelectList(db.categories, "Category_Id", "Category_Name", purchase.Category_Id);
            ViewBag.Company_Id = new SelectList(db.companies, "Company_Id", "Company_Name", purchase.Company_Id);
            ViewBag.Pay_Id = new SelectList(db.pays, "pay_Id", "Value", purchase.Pay_Id);
            ViewBag.Product_Id = new SelectList(db.products, "Product_Id", "Product_Name", purchase.Product_Id);
            return View(purchase);
        }

        // GET: purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            purchase purchase = db.purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            purchase purchase = db.purchases.Find(id);
            db.purchases.Remove(purchase);
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
