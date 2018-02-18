using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LmycDataLib.Models;

namespace LMYCWebsite.Controllers
{
    [Authorize (Roles = "Member, Admin")]
    public class BoatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Boats
        public ActionResult Index()
        {
            return View(db.boats.ToList());
        }

        // GET: Boats/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.boats.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // GET: Boats/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BoatId,BoatName,Picture,LengthInFeet,Make,Year,RecordCreationDate,CreatedBy")] Boat boat)
        {
            boat.RecordCreationDate = DateTime.Now;
            boat.CreatedBy = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.boats.Add(boat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(boat);
        }

        // GET: Boats/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.boats.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // POST: Boats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "BoatId,BoatName,Picture,LengthInFeet,Make,Year,RecordCreationDate,CreatedBy")] Boat boat)
        {
            boat.RecordCreationDate = DateTime.Now;
            boat.CreatedBy = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Entry(boat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(boat);
        }

        // GET: Boats/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boat boat = db.boats.Find(id);
            if (boat == null)
            {
                return HttpNotFound();
            }
            return View(boat);
        }

        // POST: Boats/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Boat boat = db.boats.Find(id);
            db.boats.Remove(boat);
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
