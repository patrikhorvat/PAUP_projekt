using PAUP_projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace PAUP_projekt.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index(int? IdSearch)
        {
            ViewBag.IdSearch = new SelectList(db.Kategorijas, "ID", "Ime");

            if (IdSearch == null)
            {
                return View(db.Rads.Include(r => r.KategorijaIme).Include(r => r.StrukaIme).Where(d => d.Korisnik == User.Identity.Name).ToList());
            }

            var rads = db.Rads.Include(r => r.KategorijaIme).Include(r => r.StrukaIme).Where(d => d.Korisnik == User.Identity.Name).Where(c => c.KategorijaID == IdSearch);

            return View(rads.ToList());
        }


        public ActionResult Zahtjev(int? id)
        {
            var rad = db.Rads.Find(id);
            rad.Zahtjev = true;

            db.Entry(rad).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Dodaj()
        {
            ViewBag.KategorijaID = new SelectList(db.Kategorijas, "ID", "Ime");
            ViewBag.StrukaID = new SelectList(db.Strukas, "ID", "ImeStruke");
            return View();
        }

        public void PDF()
        {
            var rad = db.Rads.Include(r => r.KategorijaIme).Include(r => r.StrukaIme).Where(c => c.Korisnik == User.Identity.Name).ToList();
            var doc = new Document();
            string path = Server.MapPath("~/Images");
            PdfWriter.GetInstance(doc, new FileStream(path + "/Doc.pdf", FileMode.Create));
            doc.Open();
            PdfPTable t = new PdfPTable(3);

            t.AddCell(new Paragraph("Ime rada:"));
            t.AddCell(new Paragraph("Ime struke:"));
            t.AddCell(new Paragraph("Ime kategorije"));

            foreach (var item in rad)
            {
                t.AddCell(new Paragraph(item.Ime));
                t.AddCell(new Paragraph(item.StrukaIme.ImeStruke));
                t.AddCell(new Paragraph(item.KategorijaIme.Ime));
            }
            doc.Add(new Paragraph("Korisnik: " + User.Identity.Name));
            doc.Add(new Paragraph(" "));
            doc.Add(t);
            doc.Add(new Paragraph(" "));

            doc.Close();
            OpenPDF(path + "/Doc.pdf");
        }

        public void OpenPDF(string s)
        {
            Response.ContentType = "Application/pdf";
            Response.TransmitFile(s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dodaj([Bind(Include = "ID,Ime,KategorijaID,StrukaID")] Rad rad, HttpPostedFileBase file)
        {
            string path = "";
            rad.Korisnik = User.Identity.Name;
            if (file != null && file.ContentLength > 0)
                try
                {
                    path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "Uspjesno stavljeni rad";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "Nema odabranog rada";
            }
            rad.Path = path;

            if (ModelState.IsValid)
            {

                db.Rads.Add(rad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategorijaID = new SelectList(db.Kategorijas, "ID", "Ime", rad.KategorijaID);
            ViewBag.StrukaID = new SelectList(db.Strukas, "ID", "ImeStruke", rad.StrukaID);

            return RedirectToAction("Index");
        }

        public ActionResult DownloadFile(int? id)
        {
            Rad rad = db.Rads.Find(id);
            string filepath = rad.Path;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Strucni Rad",
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }

        // GET: Rads/Delete/5
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

        // POST: Rads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rad rad = db.Rads.Find(id);
            db.Rads.Remove(rad);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

