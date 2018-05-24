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
    public class DonVisController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: DonVis
        public ActionResult Index()
        {
            return View(db.DonVis.ToList());
        }

        // GET: DonVis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi donVi = db.DonVis.Find(id);
            if (donVi == null)
            {
                return HttpNotFound();
            }
            return View(donVi);
        }

        // GET: DonVis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DonVis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DonVi,TenDonVi,Deleted")] DonVi donVi)
        {
            if (ModelState.IsValid)
            {
                db.DonVis.Add(donVi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(donVi);
        }

        // GET: DonVis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi donVi = db.DonVis.Find(id);
            if (donVi == null)
            {
                return HttpNotFound();
            }
            return View(donVi);
        }

        // POST: DonVis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DonVi,TenDonVi,Deleted")] DonVi donVi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donVi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(donVi);
        }

        // GET: DonVis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonVi donVi = db.DonVis.Find(id);
            if (donVi == null)
            {
                return HttpNotFound();
            }
            return View(donVi);
        }

        // POST: DonVis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonVi donVi = db.DonVis.Find(id);
            db.DonVis.Remove(donVi);
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
