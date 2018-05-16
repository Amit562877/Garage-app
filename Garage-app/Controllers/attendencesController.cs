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
    public class attendencesController : Controller
    {
        private garageEntities db = new garageEntities();

        // GET: attendences
        public ActionResult Index()
        {
            var attendences = db.attendences.Include(a => a.employee).Include(a => a.present);
            return View(attendences.ToList());
        }

        // GET: attendences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendence attendence = db.attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // GET: attendences/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name");
            ViewBag.Present_Id = new SelectList(db.presents, "Present_Id", "Value");
            return View();
        }

        // POST: attendences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Attendence_Id,Employee_Id,Present_Id,Attendence_Date")] attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.attendences.Add(attendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", attendence.Employee_Id);
            ViewBag.Present_Id = new SelectList(db.presents, "Present_Id", "Value", attendence.Present_Id);
            return View(attendence);
        }

        // GET: attendences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendence attendence = db.attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", attendence.Employee_Id);
            ViewBag.Present_Id = new SelectList(db.presents, "Present_Id", "Value", attendence.Present_Id);
            return View(attendence);
        }

        // POST: attendences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Attendence_Id,Employee_Id,Present_Id,Attendence_Date")] attendence attendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", attendence.Employee_Id);
            ViewBag.Present_Id = new SelectList(db.presents, "Present_Id", "Value", attendence.Present_Id);
            return View(attendence);
        }

        // GET: attendences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            attendence attendence = db.attendences.Find(id);
            if (attendence == null)
            {
                return HttpNotFound();
            }
            return View(attendence);
        }

        // POST: attendences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            attendence attendence = db.attendences.Find(id);
            db.attendences.Remove(attendence);
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
