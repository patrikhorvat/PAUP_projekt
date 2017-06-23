using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PAUP_FS.Models;

namespace PAUP_FS.Controllers
{
    public class Rads1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rads1
        public ActionResult Index()
        {
            var rads = db.Rads.Include(r => r.KategorijaIme).Include(r => r.StrukaIme);
            return View(rads.ToList());
        }

        // GET: Rads1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rad rad = db.Rads.Find(id);
            if (rad == null)
            {
                return HttpNotFound();
            }
            return View(rad);
        }

        // GET: Rads1/Create
        public ActionResult Create()
        {
            ViewBag.KategorijaID = new SelectList(db.Kategorijas, "ID", "Ime");
            ViewBag.StrukaID = new SelectList(db.Strukas, "ID", "ImeStruke");
            return View();
        }

        // POST: Rads1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Ime,Path,Korisnik,KategorijaID,StrukaID")] Rad rad)
        {
            if (ModelState.IsValid)
            {
                db.Rads.Add(rad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategorijaID = new SelectList(db.Kategorijas, "ID", "Ime", rad.KategorijaID);
            ViewBag.StrukaID = new SelectList(db.Strukas, "ID", "ImeStruke", rad.StrukaID);
            return View(rad);
        }

        // GET: Rads1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rad rad = db.Rads.Find(id);
            if (rad == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorijas, "ID", "Ime", rad.KategorijaID);
            ViewBag.StrukaID = new SelectList(db.Strukas, "ID", "ImeStruke", rad.StrukaID);
            return View(rad);
        }

        // POST: Rads1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Ime,Path,Korisnik,KategorijaID,StrukaID")] Rad rad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategorijaID = new SelectList(db.Kategorijas, "ID", "Ime", rad.KategorijaID);
            ViewBag.StrukaID = new SelectList(db.Strukas, "ID", "ImeStruke", rad.StrukaID);
            return View(rad);
        }

        // GET: Rads1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rad rad = db.Rads.Find(id);
            if (rad == null)
            {
                return HttpNotFound();
            }
            return View(rad);
        }

        // POST: Rads1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rad rad = db.Rads.Find(id);
            db.Rads.Remove(rad);
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
