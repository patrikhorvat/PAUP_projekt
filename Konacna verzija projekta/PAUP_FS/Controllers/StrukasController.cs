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
    public class StrukasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Strukas
        public ActionResult Index()
        {
            return View(db.Strukas.ToList());
        }

        // GET: Strukas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Struka struka = db.Strukas.Find(id);
            if (struka == null)
            {
                return HttpNotFound();
            }
            return View(struka);
        }

        // GET: Strukas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Strukas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ImeStruke")] Struka struka)
        {
            if (ModelState.IsValid)
            {
                db.Strukas.Add(struka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(struka);
        }

        // GET: Strukas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Struka struka = db.Strukas.Find(id);
            if (struka == null)
            {
                return HttpNotFound();
            }
            return View(struka);
        }

        // POST: Strukas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ImeStruke")] Struka struka)
        {
            if (ModelState.IsValid)
            {
                db.Entry(struka).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(struka);
        }

        // GET: Strukas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Struka struka = db.Strukas.Find(id);
            if (struka == null)
            {
                return HttpNotFound();
            }
            return View(struka);
        }

        // POST: Strukas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Struka struka = db.Strukas.Find(id);
            db.Strukas.Remove(struka);
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
