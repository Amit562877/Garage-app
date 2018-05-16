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
    public class salariesController : Controller
    {
        private garageEntities db = new garageEntities();

        // GET: salaries
        public ActionResult Index()
        {
            var salaries = db.salaries.Include(s => s.employee);
            return View(salaries.ToList());
        }

        // GET: salaries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary salary = db.salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // GET: salaries/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name");
            return View();
        }

        // POST: salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Salary_Id,Employee_Id,Salary_Amount,Advance_Cut,Total_Pay,Salary_Date")] salary salary)
        {
            if (ModelState.IsValid)
            {
                db.salaries.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", salary.Employee_Id);
            return View(salary);
        }

        // GET: salaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary salary = db.salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", salary.Employee_Id);
            return View(salary);
        }

        // POST: salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Salary_Id,Employee_Id,Salary_Amount,Advance_Cut,Total_Pay,Salary_Date")] salary salary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", salary.Employee_Id);
            return View(salary);
        }

        // GET: salaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salary salary = db.salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            salary salary = db.salaries.Find(id);
            db.salaries.Remove(salary);
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
