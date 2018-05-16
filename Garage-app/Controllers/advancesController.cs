using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage_app.Models;

namespace Garage_app.Controllers
{
    public class advancesController : Controller
    {
        private garageEntities db = new garageEntities();

        // GET: advances
        public async Task<ActionResult> Index()
        {
            var advances = db.advances.Include(a => a.employee);
            return View(await advances.ToListAsync());
        }

        // GET: advances/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            advance advance = await db.advances.FindAsync(id);
            if (advance == null)
            {
                return HttpNotFound();
            }
            return View(advance);
        }

        // GET: advances/Create
        public ActionResult Create()
        {
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name");
            return View();
        }

        // POST: advances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Advance_Id,Employee_Id,Amount,Advance_Date")] advance advance)
        {
            if (ModelState.IsValid)
            {
                db.advances.Add(advance);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", advance.Employee_Id);
            return View(advance);
        }

        // GET: advances/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            advance advance = await db.advances.FindAsync(id);
            if (advance == null)
            {
                return HttpNotFound();
            }
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", advance.Employee_Id);
            return View(advance);
        }

        // POST: advances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Advance_Id,Employee_Id,Amount,Advance_Date")] advance advance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(advance).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Employee_Id = new SelectList(db.employees, "Employee_Id", "Employee_Name", advance.Employee_Id);
            return View(advance);
        }

        // GET: advances/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            advance advance = await db.advances.FindAsync(id);
            if (advance == null)
            {
                return HttpNotFound();
            }
            return View(advance);
        }

        // POST: advances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            advance advance = await db.advances.FindAsync(id);
            db.advances.Remove(advance);
            await db.SaveChangesAsync();
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
