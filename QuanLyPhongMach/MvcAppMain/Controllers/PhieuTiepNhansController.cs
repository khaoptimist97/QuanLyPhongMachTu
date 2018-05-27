using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcAppMain.DTO;
using MvcAppMain.Models;
using PagedList;

namespace MvcAppMain.Controllers
{
    [Authorize]
    public class PhieuTiepNhansController : Controller
    {
        private QLPMContext db = new QLPMContext();

        // GET: PhieuTiepNhans
        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var phieuTiepNhans = from s in db.PhieuTiepNhans
                                 join a in db.HoSoBenhNhans on s.ID_BenhNhan equals a.ID_BenhNhan
                                 where s.Deleted == false
                                 select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                phieuTiepNhans = phieuTiepNhans.Where(s => s.HoSoBenhNhan.HoTen.Contains(searchString));
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(phieuTiepNhans.OrderBy(s => s.HoSoBenhNhan.HoTen).ToPagedList(pageNumber, pageSize));
        }
        public JsonResult GetSearchValue(string search)
        {
            List<BenhNhan> allsearch = db.PhieuTiepNhans.Where(s => s.HoSoBenhNhan.HoTen.Contains(search) && s.Deleted == false).Select(x => new BenhNhan
            {
                TenBenhNhan = x.HoSoBenhNhan.HoTen
            }).ToList();
            return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: PhieuTiepNhans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuTiepNhan phieuTiepNhan = db.PhieuTiepNhans.Find(id);
            if (phieuTiepNhan == null)
            {
                return HttpNotFound();
            }
            return View(phieuTiepNhan);
        }

        // GET: PhieuTiepNhans/Create
        public ActionResult Create()
        {
            PhieuTiepNhan phieu = new PhieuTiepNhan();
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans.Where(x=>x.Deleted==false), "ID_BenhNhan", "HoTen");
            List<SelectListItem> Gender = new List<SelectListItem>()
            {
               new SelectListItem
               {
                   Value = "true",
                   Text = "Nam"
               },
               new SelectListItem
               {
                   Value = "false",
                   Text = "Nữ"
               }
            };
            ViewBag.Gender = new SelectList(Gender, "Value", "Text");
            return View(phieu);
        }

        // POST: PhieuTiepNhans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhieuTiepNhan phieu)
        {
            List<SelectListItem> Gender = new List<SelectListItem>()
            {
               new SelectListItem
               {
                   Value = "true",
                   Text = "Nam"
               },
               new SelectListItem
               {
                   Value = "false",
                   Text = "Nữ"
               }
            };
            int soLuongPhieu = Convert.ToInt32(db.ThamSoes.First().GiaTri);
            if (db.PhieuTiepNhans.Where(x => x.NgayTiepNhan == phieu.NgayTiepNhan).Count() >= soLuongPhieu)
            {
                ViewBag.Error = "Đã đủ số lượng tiếp nhận trong ngày!";
                ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans.Where(x => x.Deleted == false), "ID_BenhNhan", "HoTen", phieu.ID_BenhNhan);
                ViewBag.Gender = new SelectList(Gender, "Value", "Text");
                return View(phieu);
            }
            if (ModelState.IsValid)
            {
                db.PhieuTiepNhans.Add(phieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.Gender = new SelectList(Gender, "Value", "Text");
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans.Where(x => x.Deleted == false), "ID_BenhNhan", "HoTen", phieu.ID_BenhNhan);
            return View(phieu);
        }

        // GET: PhieuTiepNhans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuTiepNhan phieuTiepNhan = db.PhieuTiepNhans.Find(id);
            if (phieuTiepNhan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans.Where(x => x.Deleted == false), "ID_BenhNhan", "HoTen", phieuTiepNhan.ID_BenhNhan);
            return View(phieuTiepNhan);
        }

        // POST: PhieuTiepNhans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDPhieuTN,ID_BenhNhan,NgayTiepNhan,Deleted")] PhieuTiepNhan phieuTiepNhan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuTiepNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_BenhNhan = new SelectList(db.HoSoBenhNhans.Where(x => x.Deleted == false), "ID_BenhNhan", "HoTen", phieuTiepNhan.ID_BenhNhan);
            return View(phieuTiepNhan);
        }
        public JsonResult ConfirmThemMoiBenhNhan(string TenBenhNhan, string GioiTinh, string NgaySinh, string DiaChi)
        {
            int newIDBenhNhan = 0;
            if (TenBenhNhan != null && DiaChi != null && GioiTinh != null && NgaySinh != null)
            {
                HoSoBenhNhan benhNhan = new HoSoBenhNhan();
                benhNhan.HoTen = TenBenhNhan;
                benhNhan.DiaChi = DiaChi;
                benhNhan.GioiTinh = Convert.ToBoolean(GioiTinh);
                benhNhan.NamSinh = Convert.ToDateTime(NgaySinh);
                db.HoSoBenhNhans.Add(benhNhan);
                db.SaveChanges();
                newIDBenhNhan = benhNhan.ID_BenhNhan;
            }
            return Json(newIDBenhNhan, JsonRequestBehavior.AllowGet);
        }
        // GET: PhieuTiepNhans/Delete/5
        public JsonResult DeleteConfirmation(int IDPhieu)
        {
            bool result = false;
            PhieuTiepNhan phieuTiep = db.PhieuTiepNhans.Find(IDPhieu);
            if (phieuTiep != null)
            {
                phieuTiep.Deleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
