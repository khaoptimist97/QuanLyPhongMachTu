using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.Models;

namespace MvcAppMain.Controllers
{
    public class ThuocsController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: Thuocs
        public ActionResult Index()
        {
            var thuocs = db.Thuocs.Include(t => t.CachDung).Include(t => t.DonVi);
            return View(thuocs.ToList());
        }

        // GET: Thuocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thuoc thuoc = db.Thuocs.Find(id);
            if (thuoc == null)
            {
                return HttpNotFound();
            }
            return View(thuoc);
        }

        // GET: Thuocs/Create
        public ActionResult Create()
        {
            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung");
            ViewBag.ID_DonVi = new SelectList(db.DonVis, "ID_DonVi", "TenDonVi");
            return View();
        }

        // POST: Thuocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Thuoc,TenThuoc,SoLuong,DonGia,ID_DonVi,ID_CachDung,Deleted")] Thuoc thuoc)
        {
            if (ModelState.IsValid)
            {
                db.Thuocs.Add(thuoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung", thuoc.ID_CachDung);
            ViewBag.ID_DonVi = new SelectList(db.DonVis, "ID_DonVi", "TenDonVi", thuoc.ID_DonVi);
            return View(thuoc);
        }

        // GET: Thuocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thuoc thuoc = db.Thuocs.Find(id);
            if (thuoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung", thuoc.ID_CachDung);
            ViewBag.ID_DonVi = new SelectList(db.DonVis, "ID_DonVi", "TenDonVi", thuoc.ID_DonVi);
            return View(thuoc);
        }

        // POST: Thuocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Thuoc,TenThuoc,SoLuong,DonGia,ID_DonVi,ID_CachDung,Deleted")] Thuoc thuoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CachDung = new SelectList(db.CachDungs, "ID_CachDung", "TenCachDung", thuoc.ID_CachDung);
            ViewBag.ID_DonVi = new SelectList(db.DonVis, "ID_DonVi", "TenDonVi", thuoc.ID_DonVi);
            return View(thuoc);
        }

        // GET: Thuocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thuoc thuoc = db.Thuocs.Find(id);
            if (thuoc == null)
            {
                return HttpNotFound();
            }
            return View(thuoc);
        }

        // POST: Thuocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thuoc thuoc = db.Thuocs.Find(id);
            db.Thuocs.Remove(thuoc);
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
